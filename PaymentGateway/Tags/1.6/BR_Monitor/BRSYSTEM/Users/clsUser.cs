using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BR.BRSYSTEM
{
    class Users
    {
    }
    class UsersInfo
    {
        private Int32 m_USERID;
        private string m_USERNAME;
        private string m_PASSWORD;
        private string m_FULLNAME;
        private int m_STATUS;
        private DateTime  m_CREATE_DATE;
        private DateTime  m_BIRTHDAY;
        private string  m_MOBILE;
        private string  m_EMAIL;
        private string  m_NOTE;
        private DateTime m_LASTDATE;
      #region //thuoc tinh cua user
      public Int32 USERID
        {
            get
            {
                return this.m_USERID;
            }
            set
            {
                this.m_USERID = value;
            }

        }
      
      public string USERNAME
      {
          get
          {
              return this.m_USERNAME;
          }
          set
          {
              this.m_USERNAME = value;
          }

      }
      

      public string PASSWORD
      {
          get
          {
              return this.m_PASSWORD;
          }
          set
          {
              this.m_PASSWORD = value;
          }

      }
     
      public string FULLNAME
      {
          get
          {
              return this.m_FULLNAME;
          }
          set
          {
              this.m_FULLNAME = value;
          }

      }
    
      public int STATUS
      {
          get
          {
              return this.m_STATUS;
          }
          set
          {
              this.m_STATUS = value;
          }

      }
      
      public DateTime CREATE_DATE
      {
          get
          {
              return this.m_CREATE_DATE;
          }
          set
          {
              this.m_CREATE_DATE = value;
          }

      }
      
      public DateTime BIRTHDAY
      {
          get
          {
              return this.m_BIRTHDAY;
          }
          set
          {
              this.m_BIRTHDAY = value;
          }

      }
      
      public string MOBILE
      {
          get
          {
              return this.m_MOBILE;
          }
          set
          {
              this.m_MOBILE = value;
          }

      }
     
      public string EMAIL
      {
          get
          {
              return this.m_EMAIL;
          }
          set
          {
              this.m_EMAIL = value;
          }

      }
      
      public string NOTE
      {
          get
          {
              return this.m_NOTE;
          }
          set
          {
              this.m_NOTE = value;
          }

      }
     
      public DateTime LASTDATE
      {
          get
          {
              return this.m_LASTDATE;
          }
          set
          {
              this.m_LASTDATE = value;
          }

      }
        #endregion






    }
}
