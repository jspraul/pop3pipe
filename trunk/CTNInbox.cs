using System;
using System.Reflection;
using System.Data;
using System.Collections.Specialized;
using System.Collections;
using System.IO;
using anmar.SharpMimeTools;
using anmar.SharpWebMail;

namespace POP3Pipe
{

    public class CTNInbox
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected DataTable inbox;
        protected DataView inbox_view;
        protected String current_folder = "inbox";

        protected Object[] names = {   "index", typeof(Int32),			            //  0
											  "msgnum", typeof(Int32),			    //  1
											  "Size", typeof(Int32),				//  2
											  "uidl", typeof(String),			    //  3
											  "From", typeof(String),			    //  4
											  "FromName", typeof(String),		    //  5
											  "FromCollection", typeof(SIEnumerable),	//  6
											  "To", typeof(String),				    //  7
											  "ToCollection", typeof(IEnumerable),	//  8
											  "Reply", typeof(IEnumerable),			//  9
											  "Subject", typeof(String),			// 10
											  "StringDate", typeof(String),		    // 11
											  "Message-ID", typeof(String),		    // 12
											  "Headers", typeof(SharpMimeHeader), 	// 13
											  "Date", typeof(DateTime),			    // 14
											  "delete", typeof(Boolean),			// 15
											  "read", typeof(Boolean),			    // 16
											  "guid", typeof(String),			    // 17
											  "folder", typeof(String)};			// 18

        protected Int32 mcount = 0;

        protected Int32 msize = 0;

        protected String sort = "msgnum DESC";

        protected IEmailClient _client = null;

        public CTNInbox()
        {
            this.init();
        }

        public CTNInbox(String[] names)
        {
            this.names = names;
        }
        public Object[] this[String uid]
        {
            get
            {
                DataRowView msg = this.GetMessageObject(uid);
                if (msg != null)
                    return msg.Row.ItemArray;
                return null;
            }
        }

        public Object[] this[Guid guid]
        {
            get
            {
                DataRowView msg = this.GetMessageObject(guid);
                if (msg != null)
                    return msg.Row.ItemArray;
                return null;
            }
        }

        public bool buildMessageTable(Int32[] list, String[] uidlist)
        {
            bool error = false;

            // We need to initialize the inbox list
            if (this.Count == 0)
            {
                for (int i = 0; i < uidlist.Length; i++)
                {
                    this.newMessage(i + 1, list[i], uidlist[i]);
                }
            }
            else
            {
                StringCollection uidl = new StringCollection();
                uidl.AddRange(uidlist);
                // As we already have an index, we try to put it in sync
                // with the mail server
                for (int i = 0; i < uidlist.Length; i++)
                {
                    this.inbox_view.RowFilter = String.Concat("uidl = '", uidlist[i], "'");
                    // Message not found, so we add it
                    if (this.inbox_view.Count == 0)
                    {
                        this.newMessage(i + 1, list[i], uidlist[i]);
                    }
                    else
                    {
                        // Message found, but at the wrong position
                        if (!this.inbox_view[0][1].Equals(i + 1))
                            this.inbox_view[0][1] = i + 1;
                        // Message found, but in the wrong folder.
                        if (!this.inbox_view[0][18].Equals(this.current_folder))
                            this.inbox_view[0][18] = this.current_folder;
                    }
                }
                // now we try to find deleted messages
                this.inbox_view.RowFilter = String.Concat("folder='", this.current_folder, "'");

                for (int i = 0; i < this.inbox.Rows.Count; i++)
                {
                    DataRow item = this.inbox.Rows[i];
                    if (!uidl.Contains(item[3].ToString()))
                    {
                        if (item[15].Equals(false))
                            this.mcount--;
                        this.msize -= (int)item[2];
                        item.Delete();
                        i--;
                    }
                }
            }
            return !error;

        }

        public bool buildMessageList(Hashtable msgs, int npage, int npagesize)
        {
            bool error = false;
            int start = (int)npage * npagesize;
            start = (start < 0) ? 0 : start;
            int end = start + npagesize;
            Int32 tmpkey;

            String field = sort.Split(' ')[0];
            // If we are sorting by any colunm which data is not contained
            // in UIDL or LIST responses, then we need to cache the whole
            // inbox in order to do the sort opetarion
            if (this.inbox.Columns[field].Ordinal > 3)
            {
                start = 0;
                end = this.Count;
            }
            this.inbox_view.RowFilter = String.Concat("delete=false AND folder='", this.current_folder, "'");
            this.inbox_view.Sort = sort;
            // Clean up message list before
            if (msgs != null && msgs.Count > 0)
                msgs.Clear();
            for (int i = start; (!error) && i < this.inbox_view.Count && i < end; i++)
            {
                tmpkey = (int)this.inbox_view[i][1];
                // We want to get headers only if we do not have them
                if (msgs != null && !msgs.ContainsKey(tmpkey) && this.inbox_view[i][13].Equals(DBNull.Value))
                {
                    msgs.Add(this.inbox_view[i][1], this.inbox_view[i][3].ToString());
                }
            }
            this.inbox_view.RowFilter = String.Empty;
            this.inbox_view.Sort = String.Empty;
            return !error;
        }

        public bool DeleteMessage(String uid)
        {
            if (this.markMessage(uid, 15, true))
            {
                this.mcount--;
                return true;
            }
            return false;
        }

        public void flush()
        {
            this.init();
        }
        private Guid getGuid(String uid)
        {
            try
            {
                return new Guid(uid);
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error("Error parsing UID", e);
                return System.Guid.Empty;
            }
        }
        public MemoryStream GetMessage(String uid)
        {
            if (this._client == null)
                return null;
            DataRowView details = this.GetMessageObject(uid);
            if (details != null)
                return this.GetMessage((int)details[1], details[3].ToString());
            else
                return null;
        }
        public MemoryStream GetMessage(int mindex, String uid)
        {
            if (this._client == null)
                return null;
            MemoryStream ms = new MemoryStream();
            if (!this._client.GetMessage(ms, mindex, uid))
            {
                // Message not found in that possition so we re-scan the server in order to find the new location
                this._client.GetFolderIndex(this, 0, 0, true);
                DataRowView details = this.GetMessageObject(uid);
                if (details == null || !this._client.GetMessage(ms, (int)details[1], details[3].ToString()))
                {
                    if (ms.CanRead)
                        ms.Close();
                    ms = null;
                }
                details = null;
            }
            return ms;
        }
        public String GetMessageFolder(String uid)
        {
            DataRowView msg = this.GetMessageObject(uid);
            if (msg != null)
                return msg[18].ToString();
            else
                return String.Empty;
        }

        public String getMessageIndex(String uid)
        {
            DataRowView msg = this.GetMessageObject(uid);
            if (msg != null)
                return msg[1].ToString();
            else
                return String.Empty;
        }
        private DataRowView GetMessageObject(String uid)
        {
            Guid guid = this.getGuid(uid);
            if (!guid.Equals(System.Guid.Empty))
                return this.GetMessageObject(new Guid(uid));
            else if (log.IsErrorEnabled)
            {
                log.Error("Error parsing UID");
            }
            return null;
        }
        private DataRowView GetMessageObject(Guid guid)
        {
            this.inbox_view.RowFilter = String.Concat("guid='", guid, "'");
            if (this.inbox_view.Count == 1)
            {
                if (log.IsDebugEnabled) log.Debug(String.Format("{0} guid='{0}' found", guid));
                return this.inbox_view[0];
            }
            else
            {
                if (log.IsDebugEnabled) log.Debug(String.Format("guid='{0}' not found", guid));
                return null;
            }
        }

        protected void init()
        {
            inbox = new DataTable("Inbox");
            for (int i = 0; i < names.Length / 2; i++)
            {
                inbox.Columns.Add(new System.Data.DataColumn((String)names[i * 2], (Type)names[i * 2 + 1]));
            }
            inbox.Columns[0].AutoIncrement = true;
            inbox.Columns[0].Unique = true;
            this.inbox_view = new DataView(this.inbox);
        }

        protected bool markMessage(String uid, int col, bool val)
        {
            DataRowView msg = this.GetMessageObject(uid);
            if (msg != null && !msg[col].Equals(val))
            {
                msg[col] = val;
                return true;
            }
            return false;
        }

        public bool newMessage(String uidl, SharpMimeHeader header)
        {
            bool error = false;
            this.inbox_view.RowFilter = String.Concat("uidl='", uidl, "'");
            if (this.inbox_view.Count == 1)
            {
                DataRowView msg = this.inbox_view[0];
                msg[4] = header.From;
                msg[5] = "";
                msg[6] = SharpMimeTools.parseFrom(header.From);
                msg[7] = header.To;
                msg[8] = SharpMimeTools.parseFrom(header.To);
                msg[9] = SharpMimeTools.parseFrom(header.Reply);
                msg[10] = SharpMimeTools.parserfc2047Header(header.Subject);
                String date = header.Date;
                if (date.Equals(String.Empty) && header.Contains("Received"))
                {
                    date = header["Received"];
                    if (date.IndexOf("\r\n") > 0)
                        date = date.Substring(0, date.IndexOf("\r\n"));
                    if (date.LastIndexOf(';') > 0)
                        date = date.Substring(date.LastIndexOf(';') + 1).Trim();
                    else
                        date = String.Empty;
                }
                msg[11] = date;
                msg[12] = header.MessageID;
                msg[13] = header;
                msg[14] = SharpMimeTools.parseDate(date);
                if (msg[6] != null)
                {
                    foreach (SharpMimeAddress item in ((IEnumerable)msg[6]))
                    {
                        msg[5] = item["name"];
                        if (msg[5] == null || msg[5].Equals(System.String.Empty))
                            msg[5] = item["address"];
                    }
                }
            }
            else
            {
                error = true;
            }
            return !error;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="uidl"></param>
        public void newMessage(System.Int32 index, System.Int32 size, System.String uidl)
        {
            System.Data.DataRow tmpRow;
            tmpRow = inbox.NewRow();

            tmpRow[1] = index;
            tmpRow[2] = size;
            tmpRow[3] = uidl;
            tmpRow[12] = System.String.Empty;
            tmpRow[13] = System.DBNull.Value;
            tmpRow[15] = false;
            tmpRow[16] = false;
            tmpRow[17] = System.Guid.NewGuid().ToString();
            tmpRow[18] = this.current_folder;
            inbox.Rows.Add(tmpRow);
            this.mcount++;
            this.msize += size;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        public void readMessage(System.String uid)
        {
            this.markMessage(uid, 16, true);
            return;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        public void undeleteMessage(System.String uid)
        {
            if (this.markMessage(uid, 15, false))
                this.mcount++;
            return;
        }
        /// <summary>
        /// 
        /// </summary>
        public anmar.SharpWebMail.IEmailClient Client
        {
            get { return this._client; }
            set { this._client = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public System.Data.DataView Inbox
        {
            get
            {
                return this.inbox_view;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 Count
        {
            get
            {
                return this.inbox.Rows.Count;
            }
        }
        public System.String CurrentFolder
        {
            get
            {
                return this.current_folder;
            }
            set
            {
                this.current_folder = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 MessageCount
        {
            get
            {
                return this.mcount;
            }
            set
            {
                this.mcount = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 MessageSize
        {
            get
            {
                return this.msize;
            }
            set
            {
                this.msize = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public System.String SortExpression
        {
            get
            {
                return this.sort;
            }
            set
            {
                if (value != null && value.Length > 0)
                {
                    System.String field = value.Split(' ')[0];
                    if (this.inbox.Columns.Contains(field))
                        this.sort = value;
                }
            }
        }
    }
}
