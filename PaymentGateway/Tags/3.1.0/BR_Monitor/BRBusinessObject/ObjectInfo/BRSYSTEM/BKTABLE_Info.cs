using System.Diagnostics;
using System;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace BR.BRBusinessObject
{
    public class BKTABLEInfo
    {
      private string _SOURCETBL;
      private string _DESTBL;
      private string _BKTYPE;
      private string _BKTIME;
      private string _FILENAME;
      private DateTime _LASTCLEAN;
      private DateTime _LASTEXP;
      private int _ID;

      public BKTABLEInfo()
		{
			
		}
      public string SOURCETBL
      {
          get
          {
              return _SOURCETBL;
          }
          set
          {
              _SOURCETBL = value;
          }
      }
      public string DESTBL
      {
          get
          {
              return _DESTBL;
          }
          set
          {
              _DESTBL = value;
          }
      }
      public string BKTYPE
      {
          get
          {
              return BKTYPE;
          }
          set
          {
              _BKTYPE = value;
          }
      }
      public string BKTIME
      {
          get
          {
              return _BKTIME;
          }
          set
          {
              _BKTIME = value;
          }
      }
      public string FILENAME
      {
          get
          {
              return _FILENAME;
          }
          set
          {
              _FILENAME = value;
          }
      }
      public DateTime LASTCLEAN
      {
          get
          {
              return _LASTCLEAN;
          }
          set
          {
              _LASTCLEAN = value;
          }
      }
      public DateTime LASTEXP
      {
          get
          {
              return _LASTEXP;
          }
          set
          {
              _LASTEXP = value;
          }
      }
      public int ID
      {
          get
          {
              return _ID;
          }
          set
          {
              _ID = value;
          }
      }
    }
}
