// DllASCIIAS400.h
#include <windows.h>
#include <stdlib.h>	
#include <stdio.h>
#include <time.h>
#include <io.h>
#include <malloc.h>

#include <iostream>
#include <process.h>	
#include <winsock.h>
//#include <stdlib.h>
//#include <winsock2.h>
#pragma comment(lib, "ws2_32")


#include "stdafx.h"
using namespace System;
using namespace System::Threading;
using namespace System::Diagnostics;
using namespace System::IO;

using namespace System::Collections::Generic ;
using namespace System::Runtime::InteropServices;
using namespace System::Reflection;
using namespace System::Collections;
using namespace System::Data;

#pragma once

using namespace System;

namespace DllASCIIAS400 {

#define GW_MAX_LENGTH_STRING		4096
#define CV_MAX_LENGTH_STRING		4600
#define CV_MAX_SIBS_LENGTH_STRING	4096
#define GW_TIMES_TRY_TO_CONNECT				3					// before update message to can't send
#define GW_TIMES_TRY_GET_BROKEN_MSG 2
// Define log level
#define GW_INFORMATION		1
#define GW_WARNING			2
#define GW_ERROR			3

#define GW_MAX_SIBS_LENGTH_STRING	4096
#define GW_SECOND_WAIT_MESSAGE		0
#define GW_MILISECOND_WAIT_MESSAGE	100
// Define data type
#define GW_TYPE_CHAR		0
#define GW_TYPE_ZONED		1
#define GW_TYPE_PACKED		2
#define GW_TYPE_BINARY		3
#define GW_TYPE_NUMBER		4
#define GW_TYPE_DATE		5

// Define string length

#define CV_SQL_LEN				600
#define	CV_MAX_GETVAL_LEN		2000
#define CV_MAX_DATE_LEN			30

// Define for log file
#define	LOG_FILE_NAME				"SWIFT_ErrLog.txt"
#define	HIST_FILE_NAME				"Hist_SWIFT_ErrLog.txt"
#define	MAX_LOG_FILE_SIZE			200000


	public ref class CCMessage
	{
		public:
		SOCKET			mSocket;
		bool			mbSocketOK;	
	
		void CreateSocket(char* mszSIBSIPAddress ,unsigned short mulSIBSIPPort);

		
		// nhan dien ve tu SIBS
		String^ ReceiveMessageASCII(DataTable^ tblMSG_DEF, 
											String^ strMsgType,
											long &DetailStartPos,
											long &DetailLen,
											long &DetailNumber);

		char* ReceiveMessagechar(String^ strReceive);

		//long ReceiveMessage(char* szReceive);
		long ReceiveMessage(char* szMsgRequest);

		bool SendMessage(	char* szConverted,
							char* szSIBSAddress ,
							unsigned short szSIBSPort, 
							long msglength);
	
		//bool SendMessage(String^ mszSIBSIPAddress ,unsigned short szSIBSPort, String^ strConverted, long msglength);

		//Cobvert du lieu sang chuan AS400
		bool ConvertToAS400_Table(	String^ mszSIBSIPAddress ,
										unsigned short szSIBSPort, 
										DataTable^ tblConvert);
		
		String^ ConvertFieldAs400(String^ szValueIN, 
									 long lFieldLength, 
									 long lDataType);
		
		char* ConvertFieldToAs400(String^ szValueIN, 
									 long lFieldLength, 
									 long lDataType);

		void ConvertField400(	char* szValueIN,
								long &lFieldLength,
								long lDataType);

		//Convert du lieu sang ASCII
		String^ ConvertFieldAscii(String^ szValueIN,
								  long lFieldLength, 
								  long lDataType,
								  long lOption);

		char* ConvertFieldToAscii(String^ szValueIN,
								  long lFieldLength, 
								  long lDataType,
								  long lOption);
		
		void ConvertFieldToAscii(char* szValue,
								  long lFieldLength, 
								  long lDataType,
								  long lOption);

		long logfilesize;

		long lMSG_status;

		void GetValue(char* strSource,
							char* strReturn,
							long start,
							long len);
		bool PumpIn(	char* strDes,
						const char *strSource, 
						long start, 
						long len);
	
	


		
		// TODO: Add your methods for this class here.
	};
}
