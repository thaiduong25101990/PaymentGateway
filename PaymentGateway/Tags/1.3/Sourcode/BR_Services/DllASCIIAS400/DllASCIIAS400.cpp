// This is the main DLL file.

#include "stdafx.h"
#include "DllASCIIAS400.h"
#include "PDECIMAL.H"
#include <vcclr.h>
#include <windows.h>
#include <time.h>
#include <stdio.h>
#include <math.h>
#include <string> 
#include <fstream>
#include <string.h>


using namespace System;
using namespace System::Threading;
using namespace System::Diagnostics;
using namespace System::IO;

using namespace System::Collections::Generic ;
using namespace System::Runtime::InteropServices;
using namespace System::Reflection;
using namespace System::Data;
using namespace System::Collections;
using namespace DllASCIIAS400;

namespace DllASCIIAS400
{
	void CCMessage::GetValue(char* strSource,
							char* strReturn,
							long start,
							long len)
{
	int i;
	for (i=0;i<len;i++)
	{
		strReturn[i]=strSource[start+i-1];
	}
		strReturn[i]='\0';
}

/*************************************************************************************
 * ++
 * Method name		: CCMessage::PumpIn(char *dest, 
 *											  const char *source, 
 *											  long start, 
 *											  long len)
 *
 * Description		: Put a char* to a predefine position in other char*
 *
 * Parameter:		: char * strDes			: destination char*
 *					  const char * strSource	: char* to pump
 *					  long start			: start position to pump
 *					  long len				: length of the pump char*
 *
 * Global variable:
 *		Accessed	: None
 *		Modified	: None
 *
 * Return:			: true if succeed, otherwise, false
 *
 * Modifications	: QuanLD
 *                                 Function first created
 *                                 SIBS-IBPS-GW 
 *                                 Pump a string into another
 * --
 *************************************************************************************/
	bool CCMessage::PumpIn(char* strDes, const char *strSource, long start, long len)
{
	for(int i = 0; i < len; i++)
	{
		strDes[start + i-1] = strSource[i];
	}
	return(true);
}




	/*************************************************************************************
 * ++
 * Method name		: CMessage::ConvertFieldAs400
 *
 * Description		: Convert a string in Ascii to As400 data type
 *
 * Parameter:		: char *szValue, long lFieldLength, 
 *					  long lDataType
 *					  
 * Global variable	: None
 *		Accessed	: None
 *		Modified	: None
 *
 * Return:			: void
 *
 * Created date		: 2.Jan.03  - Nguyen Quang Lan
 * --
 *************************************************************************************/
	void  CCMessage::ConvertField400(char *szValue, 
								  long &lFieldLength, 
								  long lDataType)
{
		
	double	dTempNumber;
	char	szFormat[CV_MAX_DATE_LEN];
	char	szTemp[CV_MAX_GETVAL_LEN + 1];
	
	switch (lDataType)
	{
	case GW_TYPE_CHAR:
		ASCII_to_EBCDIC(lFieldLength, (unsigned char*)szValue);
		break;
	case GW_TYPE_ZONED:
		dTempNumber = atof(szValue);
		sprintf(szFormat, "%s%d%d.%df", "%", 0, lFieldLength, 0);
		sprintf(szValue, szFormat, dTempNumber);
		atos((unsigned char*)szTemp, (unsigned char*)szValue, lFieldLength);
		strcpy(szValue, szTemp);
		break;
	case GW_TYPE_PACKED:
		dTempNumber = atof(szValue);
		sprintf(szFormat, "%s%d%d.%df", "%", 0, lFieldLength, 0);
		sprintf(szValue, szFormat, dTempNumber);
		atop((unsigned char*)szTemp, (lFieldLength+2)/2, (unsigned char*)szValue, lFieldLength);
		lFieldLength=(lFieldLength+2)/2;
		break;
	case GW_TYPE_BINARY:
		dTempNumber = atof(szValue);
		sprintf(szFormat, "%s%d%d.%df", "%", 0, lFieldLength, 0);
		sprintf(szValue, szFormat, dTempNumber);
		atob((unsigned char*) szTemp,4, (unsigned char*) szValue, lFieldLength);
		lFieldLength=4;
	}

	if(lDataType==GW_TYPE_PACKED || lDataType==GW_TYPE_BINARY)
	{
		for(int i=0; i<lFieldLength; i++)
		{
			szValue[i] = szTemp[i];
		}
	}
	

}

char* CCMessage::ConvertFieldToAs400(String^ szValueIN, 
									 long lFieldLength, 
									 long lDataType)
{

	char *szValue = (char*)(void*)Marshal::StringToHGlobalAnsi(szValueIN);

	double	dTempNumber;
	char	szFormat[CV_MAX_DATE_LEN];
	char	szTemp[CV_MAX_GETVAL_LEN + 1];
	
	switch (lDataType)
	{
	case GW_TYPE_CHAR:
		ASCII_to_EBCDIC(lFieldLength, (unsigned char*)szValue);
		break;
	case GW_TYPE_ZONED:
		atos((unsigned char*)szTemp, (unsigned char*)szValue, lFieldLength);
		strcpy(szValue, szTemp);
		break;
	case GW_TYPE_PACKED:
		dTempNumber = atof(szValue);
		sprintf(szFormat, "%s%d%d.%df", "%", 0, lFieldLength, 0);
		sprintf(szValue, szFormat, dTempNumber);
		atop((unsigned char*)szTemp, (lFieldLength+2)/2, (unsigned char*)szValue, lFieldLength);
		lFieldLength=(lFieldLength+2)/2;
		break;
	case GW_TYPE_BINARY:
		dTempNumber = atof(szValue);
		sprintf(szFormat, "%s%d%d.%df", "%", 0, lFieldLength, 0);
		sprintf(szValue, szFormat, dTempNumber);
		atob((unsigned char*) szTemp,4, (unsigned char*) szValue, lFieldLength);
		lFieldLength=4;
	}
	
	if(lDataType==GW_TYPE_PACKED || lDataType==GW_TYPE_BINARY)
	{
		for(int i=0; i<lFieldLength; i++)
		{	
			szValue[i]=szTemp[i];
		}
	}
	return szValue;
}









String^ CCMessage::ConvertFieldAs400(String^ szValueIN, 
									 long lFieldLength, 
									 long lDataType)

{

	char *szValue = (char*)(void*)Marshal::StringToHGlobalAnsi(szValueIN);

	double	dTempNumber;
	char	szFormat[CV_MAX_DATE_LEN];
	char	szTemp[CV_MAX_GETVAL_LEN + 1];
	
	switch (lDataType)
	{
	case GW_TYPE_CHAR:
		ASCII_to_EBCDIC(lFieldLength, (unsigned char*)szValue);
		break;
	case GW_TYPE_ZONED:
		atos((unsigned char*)szTemp, (unsigned char*)szValue, lFieldLength);
		strcpy(szValue, szTemp);
		break;
	case GW_TYPE_PACKED:
		dTempNumber = atof(szValue);
		sprintf(szFormat, "%s%d%d.%df", "%", 0, lFieldLength, 0);
		sprintf(szValue, szFormat, dTempNumber);
		atop((unsigned char*)szTemp, (lFieldLength+2)/2, (unsigned char*)szValue, lFieldLength);
		lFieldLength=(lFieldLength+2)/2;
		break;
	case GW_TYPE_BINARY:
		dTempNumber = atof(szValue);
		sprintf(szFormat, "%s%d%d.%df", "%", 0, lFieldLength, 0);
		sprintf(szValue, szFormat, dTempNumber);
		atob((unsigned char*) szTemp,4, (unsigned char*) szValue, lFieldLength);
		lFieldLength=4;
	}
	
	String^ strout;
	if(lDataType==GW_TYPE_PACKED || lDataType==GW_TYPE_BINARY)
	{
		for(int i=0; i<lFieldLength; i++)
		{	
			String^ ms = Marshal::PtrToStringAnsi(static_cast<IntPtr>(szTemp+i));
			if (ms->Length == 0)
			{
				strout = strout + " ";
			}
			else
			{
				if (ms->Length >1)
					ms = ms->Substring(0,1);
				strout = strout + ms;
				delete ms;
			}
		}

	return strout;
	}
	String^ strReturn=gcnew String(szValue);
//	delete[] szValue;
	return strReturn;
}



 


/*************************************************************************************
 * ++
 * Method name		: CMessage::ConvertFieldAscii
 *
 * Description		: Convert a string in As400 data type to Ascii
 *
 * Parameter:		: char *szValue, long lFieldLength, 
 *					  long lDataType, long Option
 *					  
 * Global variable	: None
 *		Accessed	: None
 *		Modified	: None
 *
 * Return:			: void
 *
 * Created date		: 2.Jan.03  - QuanLD
 * --
 *************************************************************************************/

String^ CCMessage::ConvertFieldAscii(String^ szValueIN,
								  long lFieldLength, 
								  long lDataType,long lOption)
{
	double	dTempNumber;
	char	szFormat[CV_MAX_DATE_LEN];
	char	szTemp[CV_MAX_GETVAL_LEN];
	int		i;
	char *szValue = (char*)(void*)Marshal::StringToHGlobalAnsi(szValueIN);
	if (lOption==0)
	{
		switch (lDataType)
		{
		case GW_TYPE_CHAR:
			EBCDIC_to_ASCII(lFieldLength, (unsigned char*)szValue);
			break;
		case GW_TYPE_ZONED:
			stoa((unsigned char*)szTemp, (unsigned char*)szValue, lFieldLength);
			strcpy(szValue, szTemp);
			break;
		case GW_TYPE_PACKED:
			ptoa((unsigned char*)szTemp, lFieldLength, (unsigned char*)szValue, (lFieldLength+2)/2);
			dTempNumber=atof(szTemp);
			sprintf(szFormat, "%s%d%d.%df", "%", 0, lFieldLength, 0);
			sprintf(szValue, szFormat, dTempNumber);
			break;
		case GW_TYPE_BINARY:
			btoa((unsigned char*) szTemp,lFieldLength, (unsigned char*) szValue, 4);
			strcpy(szValue, szTemp);
		}
	}
	else //convert with null szValue
	{
		if(lDataType==GW_TYPE_CHAR)
		{
			for(i = 0; i < (int)lFieldLength; i++)
			{
				szValue[i] = ' ';
			}
			szValue[lFieldLength] = '\0';
		}
		else
		{
			szValue[0]='\0';
			dTempNumber = 0;
			sprintf(szFormat, "%s%d%d.%df", "%", 0, lFieldLength, 0);
			sprintf(szValue, szFormat, dTempNumber);
		}
		/*switch (lDataType)
		{
		case GW_TYPE_CHAR:
			for(i = 0; i < (int)lFieldLength; i++)
			{
				szValue[i] = ' ';
			}
			szValue[lFieldLength] = '\0';
			break;
		case GW_TYPE_ZONED:
			szValue[0]='\0';
			dTempNumber = 0;
			sprintf(szFormat, "%s%d%d.%df", "%", 0, lFieldLength, 0);
			sprintf(szValue, szFormat, dTempNumber);
			break;
		case GW_TYPE_PACKED:
			szValue[0]='\0';
			dTempNumber=0;
			sprintf(szFormat, "%s%d%d.%df", "%", 0, lFieldLength, 0);
			sprintf(szValue, szFormat, dTempNumber);
			break;
		case GW_TYPE_BINARY:
			szValue[0]='\0';
			dTempNumber=0;
			sprintf(szFormat, "%s%d%d.%df", "%", 0, lFieldLength, 0);
			sprintf(szValue, szFormat, dTempNumber);
			break;
		}*/
	}
	return gcnew String(szValue);
}





 


/*************************************************************************************
 * ++
 * Method name		: CMessage::ConvertFieldAscii
 *
 * Description		: Convert a string in As400 data type to Ascii
 *
 * Parameter:		: char *szValue, long lFieldLength, 
 *					  long lDataType, long Option
 *					  
 * Global variable	: None
 *		Accessed	: None
 *		Modified	: None
 *
 * Return:			: void
 *
 * Created date		: 2.Jan.03  - QuanLD
 * --
 *************************************************************************************/

char* CCMessage::ConvertFieldToAscii(String^ szValueIN,
								  long lFieldLength, 
								  long lDataType,
								  long lOption)
{
	double	dTempNumber;
	char	szFormat[CV_MAX_DATE_LEN];
	char	szTemp[CV_MAX_GETVAL_LEN];
	int		i;
	char *szValue = (char*)(void*)Marshal::StringToHGlobalAnsi(szValueIN);
	if (lOption==0)
	{
		switch (lDataType)
		{
		case GW_TYPE_CHAR:
			EBCDIC_to_ASCII(lFieldLength, (unsigned char*)szValue);
			break;
		case GW_TYPE_ZONED:
			stoa((unsigned char*)szTemp, (unsigned char*)szValue, lFieldLength);
			strcpy(szValue, szTemp);
			break;
		case GW_TYPE_PACKED:
			ptoa((unsigned char*)szTemp, lFieldLength, (unsigned char*)szValue, (lFieldLength+2)/2);
			dTempNumber=atof(szTemp);
			sprintf(szFormat, "%s%d%d.%df", "%", 0, lFieldLength, 0);
			sprintf(szValue, szFormat, dTempNumber);
			break;
		case GW_TYPE_BINARY:
			btoa((unsigned char*) szTemp,lFieldLength, (unsigned char*) szValue, 4);
			strcpy(szValue, szTemp);
		}
	}
	else //convert with null szValue
	{
		switch (lDataType)
		{
		case GW_TYPE_CHAR:
			for(i = 0; i < (int)lFieldLength; i++)
			{
				szValue[i] = ' ';
			}
			szValue[lFieldLength] = '\0';
			break;
		case GW_TYPE_ZONED:
			szValue[0]='\0';
			dTempNumber = 0;
			sprintf(szFormat, "%s%d%d.%df", "%", 0, lFieldLength, 0);
			sprintf(szValue, szFormat, dTempNumber);
			break;
		case GW_TYPE_PACKED:
			szValue[0]='\0';
			dTempNumber=0;
			sprintf(szFormat, "%s%d%d.%df", "%", 0, lFieldLength, 0);
			sprintf(szValue, szFormat, dTempNumber);
			break;
		case GW_TYPE_BINARY:
			szValue[0]='\0';
			dTempNumber=0;
			sprintf(szFormat, "%s%d%d.%df", "%", 0, lFieldLength, 0);
			sprintf(szValue, szFormat, dTempNumber);
			break;
		}
	}
	return szValue;
}





/*************************************************************************************
 * ++
 * Method name		: CMessage::ConvertFieldAscii
 *
 * Description		: Convert a string in As400 data type to Ascii
 *
 * Parameter:		: char *szValue, long lFieldLength, 
 *					  long lDataType, long Option
 *					  
 * Global variable	: None
 *		Accessed	: None
 *		Modified	: None
 *
 * Return:			: void
 *
 * Created date		: 2.Jan.03  - QuanLD
 * --
 *************************************************************************************/

void CCMessage::ConvertFieldToAscii(char* szValue,
								  long lFieldLength, 
								  long lDataType,
								  long lOption)
{
	double	dTempNumber;
	char	szFormat[CV_MAX_DATE_LEN];
	char	szTemp[CV_MAX_GETVAL_LEN];
	int		i;
	if (lOption==0)
	{
		switch (lDataType)
		{
		case GW_TYPE_CHAR:
			EBCDIC_to_ASCII(lFieldLength, (unsigned char*)szValue);
			break;
		case GW_TYPE_ZONED:
			stoa((unsigned char*)szTemp, (unsigned char*)szValue, lFieldLength);
			strcpy(szValue, szTemp);
			break;
		case GW_TYPE_PACKED:
			ptoa((unsigned char*)szTemp, lFieldLength, (unsigned char*)szValue, (lFieldLength+2)/2);
			dTempNumber=atof(szTemp);
			sprintf(szFormat, "%s%d%d.%df", "%", 0, lFieldLength, 0);
			sprintf(szValue, szFormat, dTempNumber);
			break;
		case GW_TYPE_BINARY:
			btoa((unsigned char*) szTemp,lFieldLength, (unsigned char*) szValue, 4);
			strcpy(szValue, szTemp);
		}
	}
	else //convert with null szValue
	{
		switch (lDataType)
		{
		case GW_TYPE_CHAR:
			for(i = 0; i < (int)lFieldLength; i++)
			{
				szValue[i] = ' ';
			}
			szValue[lFieldLength] = '\0';
			break;
		case GW_TYPE_ZONED:
			szValue[0]='\0';
			dTempNumber = 0;
			sprintf(szFormat, "%s%d%d.%df", "%", 0, lFieldLength, 0);
			sprintf(szValue, szFormat, dTempNumber);
			break;
		case GW_TYPE_PACKED:
			szValue[0]='\0';
			dTempNumber=0;
			sprintf(szFormat, "%s%d%d.%df", "%", 0, lFieldLength, 0);
			sprintf(szValue, szFormat, dTempNumber);
			break;
		case GW_TYPE_BINARY:
			szValue[0]='\0';
			dTempNumber=0;
			sprintf(szFormat, "%s%d%d.%df", "%", 0, lFieldLength, 0);
			sprintf(szValue, szFormat, dTempNumber);
			break;
		}
	}
	//return szValue;
}




/*****************************************************************
 * ++
 * Method name:		SendMessage(char* szConverted)
 *
 * Description:		- Check if socket is not created yet (mbSocketOK=false), CreateSocket
 *					- Send string szConverted, set socket status to mbSocketOK
 *
 * Parameter:		- mszFileName: char* content filename to read
 *
 * Global variable:
 *		Accessed:	None
 *		Modified:	mbSocketOK
 *					mSocket
 *
 * Return:			GW_SENT if send successfully
 *					GW_CONVERTED if send false
 *
 * Modifications	: QuanLD
 *                                 Function first created
 *                                 SIBS-IBPS-GW 
 *                                 Get Registry Information
 * --
 ******************************************************************/

/*****************************************************************
 * ++
 * Method name:		SendMessage(char* szConverted)
 *
 * Description:		- Check if socket is not created yet (mbSocketOK=false), CreateSocket
 *					- Send string szConverted, set socket status to mbSocketOK
 *
 * Parameter:		- mszFileName: char* content filename to read
 *
 * Global variable:
 *		Accessed:	None
 *		Modified:	mbSocketOK
 *					mSocket
 *
 * Return:			GW_SENT if send successfully
 *					GW_CONVERTED if send false
 *
 * Modifications	: QuanLD
 *                                 Function first created
 *                                 SIBS-IBPS-GW 
 *                                 Get Registry Information
 * --
 ******************************************************************/
//

bool CCMessage::SendMessage( char* szConverted,char* szSIBSAddress ,unsigned short szSIBSPort, long msglength)

{


	int		iReturn;
	bool	bStatus=true;

	if(!mbSocketOK)
	{
		CreateSocket(szSIBSAddress,szSIBSPort);
		if(!mbSocketOK)
		{
			bStatus = false;
		}
	}

	if(mbSocketOK)
	{
		iReturn = send(mSocket, szConverted, msglength, 0); //lanNQ modify

		// If send false, recreate and try again
		if(iReturn == SOCKET_ERROR)
		{
			CreateSocket(szSIBSAddress,szSIBSPort);
			iReturn = send(mSocket, szConverted, msglength, 0); //lanNQ modify
			if(iReturn == SOCKET_ERROR)
			{
				mbSocketOK = false;
				bStatus = false;
			}
		}
	}
	//return(bStatus);



	//int		iReturn;
	//bool	bStatus=true;
	//
	//if(!mbSocketOK)
	//{
	//	CreateSocket(szSIBSAddress,szSIBSPort);
	//	if(!mbSocketOK)
	//	{
	//		bStatus = false;
	//	}
	//}

	//if(mbSocketOK)
	//{
	//	iReturn = send(mSocket, szConverted, msglength, 0); //lanNQ modify

	//	// If send false, recreate and try again
	//	if(iReturn == SOCKET_ERROR)
	//	{
	//		CreateSocket(szSIBSAddress,szSIBSPort);
	//		iReturn = send(mSocket, szConverted, msglength, 0); //lanNQ modify
	//		if(iReturn == SOCKET_ERROR)
	//		{
	//			mbSocketOK = false;
	//			bStatus = false;
	//		}
	//	}
	//}
	
	/*char szFilename[20];
	FILE* pFile;
	sprintf(szFilename, "C:\\Temp\\Test111.txt\0", 1000);
	pFile = fopen(szFilename,"w");
	fwrite(szConverted,1,msglength,pFile);
	fclose(pFile);*/

	return(bStatus);
}



 //
///*****************************************************************
// * ++
// * Method name:		ReceiveMessage(char*)
// *
// * Description:		Receive message sent from the HOST, first 4 bytes
// *					content total length of the message must be received
// *
// * Global variable:
// *					mSocket
// *					mbSocketOK
// *
// * Return:			Size of the message received
// *
// * Modifications	: QuanLD
// *                                 Function first created
// *                                 SIBS-IBPS-GW 
// *                                 Create new socket
// * --
// ******************************************************************/
	String^ CCMessage::ReceiveMessageASCII(DataTable^ tblMSG_DEF, 
											String^ strMsgType,
											long &DetailStartPos,
											long &DetailLen,
											long &DetailNumber)
	{
		long itmp;
		long i=0;
		VARIANT_BOOL	vbEOF=FALSE;
		char			szConverted[CV_MAX_LENGTH_STRING];
		char			szValue[3500];
		long			lFieldLength,lTempFieldLength, lFieldPos,lFieldGWPos,lDataType,lMsgLength,lAddLength,lCheckLength,
						lHeadLength,lHeadGWLength,lDetailLength,lDetailGWLength,lDetailNumber;
		long InLen;
		String^ strResult="";
		
		char strIn[CV_MAX_LENGTH_STRING];
		InLen = CCMessage::ReceiveMessage(strIn);
		if (InLen ==0)
		{
			char* ex;
			throw ex;
		}

		memset(szConverted,' ',CV_MAX_LENGTH_STRING);

		if(strMsgType =="MB55901R" || strMsgType=="MB55903R")
			{
				lAddLength=10;	
			}
			else
			{
				lAddLength=0;
			}

			try
			{
				//get HeadLength & DetailLength
				if (tblMSG_DEF->Rows->Count ==0)
					return " ";
				if (tblMSG_DEF->Rows[0]["SIBS_Head_Length"]->ToString()!="")
					lHeadLength=Convert::ToInt32(tblMSG_DEF->Rows[0]["SIBS_Head_Length"]->ToString());
				
				if (tblMSG_DEF->Rows[0]["GW_Head_Length"]->ToString()!="")
					lHeadGWLength=Convert::ToInt32(tblMSG_DEF->Rows[0]["GW_Head_Length"]->ToString());

				if (tblMSG_DEF->Rows[0]["SIBS_Detail_Length"]->ToString()!="")
					lDetailLength=Convert::ToInt32(tblMSG_DEF->Rows[0]["SIBS_Detail_Length"]->ToString());
				if (tblMSG_DEF->Rows[0]["GW_Detail_Length"]->ToString()!="")
					lDetailGWLength=Convert::ToInt32(tblMSG_DEF->Rows[0]["GW_Detail_Length"]->ToString());
		
				
				lHeadLength += lAddLength;
				lHeadGWLength += lAddLength;
				lDetailLength -= lAddLength;
				lDetailGWLength -= lAddLength;

				lDetailNumber = (long)ceil(((double)InLen-(double)lHeadLength)/(double) lDetailLength);
				
				lMsgLength=0;

				
				while(i< tblMSG_DEF->Rows->Count)
				{
					if (tblMSG_DEF->Rows[i]["Length"]->ToString()!="")
						lFieldLength=Convert::ToInt32(tblMSG_DEF->Rows[i]["Length"]->ToString());

					if (tblMSG_DEF->Rows[i]["SIBS_Pos"]->ToString()!="")
						lFieldPos=Convert::ToInt32(tblMSG_DEF->Rows[i]["SIBS_Pos"]->ToString());
					
					if (tblMSG_DEF->Rows[i]["GW_Pos"]->ToString()!="")
						lFieldGWPos=Convert::ToInt32(tblMSG_DEF->Rows[i]["GW_Pos"]->ToString());
					
					if (tblMSG_DEF->Rows[i]["Data_Type"]->ToString()!="")
						lDataType=Convert::ToInt32(tblMSG_DEF->Rows[i]["Data_Type"]->ToString());
				
					
					
					if (lMsgLength<lHeadLength) //Convert cho Header
					{
						switch(lDataType)
						{
						case GW_TYPE_PACKED:
							CCMessage::GetValue(strIn,szValue,lFieldPos,(lFieldLength+2)/2);
							break;
						case GW_TYPE_BINARY: 
							CCMessage::GetValue(strIn,szValue,lFieldPos,4);
							break;				
						default:
							CCMessage::GetValue(strIn,szValue,lFieldPos,lFieldLength);
							break;
						}
					
						CCMessage::ConvertFieldToAscii(szValue, lFieldLength,lDataType,0);
						CCMessage::PumpIn(szConverted,szValue, lFieldGWPos , strlen(szValue));		
					}
					else //Convert cho Detail
					{
						for (int i=0;i<lDetailNumber;i++)
						{
							switch(lDataType)
							{
							case GW_TYPE_PACKED:
								lCheckLength=lFieldPos+lDetailLength*i+ (lFieldLength+2)/2;
								if(lCheckLength<=InLen)
								{
									CCMessage::GetValue(strIn,szValue,lFieldPos+lDetailLength*i,(lFieldLength+2)/2);								
									lTempFieldLength=lFieldLength;
								}
								else
								{
									lTempFieldLength=InLen-(lFieldPos+lDetailLength*i) + 1;
									if(lTempFieldLength>0)
									{
										CCMessage::GetValue(strIn,szValue,lFieldPos+lDetailLength*i,lTempFieldLength);
									}
								}
								break;
							case GW_TYPE_BINARY: 
								lCheckLength=lFieldPos+lDetailLength*i+ 4;
								if(lCheckLength<=InLen)
								{
									CCMessage::GetValue(strIn,szValue,lFieldPos+lDetailLength*i,4);
									lTempFieldLength=lFieldLength;
								}
								else
								{
									lTempFieldLength=InLen-(lFieldPos+lDetailLength*i) + 1;
									if(lTempFieldLength>0)
									{
										CCMessage::GetValue(strIn,szValue,lFieldPos+lDetailLength*i,lTempFieldLength);								
									}
								}
								break;				
							default:
								lCheckLength=lFieldPos+lDetailLength*i+ lFieldLength;
								if(lCheckLength<=InLen)
								{
									CCMessage::GetValue(strIn,szValue,lFieldPos+lDetailLength*i,lFieldLength);
									lTempFieldLength=lFieldLength;
								}
								else
								{
									lTempFieldLength=InLen-(lFieldPos+lDetailLength*i) + 1;
									if(lTempFieldLength>0)
									{
										CCMessage::GetValue(strIn,szValue,lFieldPos+lDetailLength*i,lTempFieldLength);								
									}
								}
								break;
							}

							if(lTempFieldLength>0)
							{
								if(lFieldPos+lDetailLength*i<=InLen)
								{					
									CCMessage::ConvertFieldToAscii(szValue, lTempFieldLength,lDataType,0);
								}
								else
								{
									CCMessage::ConvertFieldToAscii(szValue, lTempFieldLength,lDataType,1);
								}
							}
							else
							{
								strcpy(szValue, "");
							}

							CCMessage::PumpIn(szConverted,szValue, lFieldGWPos+lDetailGWLength*i , strlen(szValue));
						}
					}

					lMsgLength=lFieldGWPos+lFieldLength-1;

					i=i+1;
					
				}
				lMsgLength=lHeadGWLength+lDetailGWLength*lDetailNumber;
				szConverted[lMsgLength]='\0';
				DetailStartPos=lHeadGWLength+1;
				DetailLen=lDetailGWLength;
				DetailNumber=lDetailNumber;

				
				strResult= Marshal::PtrToStringAnsi(static_cast<IntPtr>(szConverted));
				
			}
			catch (char* ex)
			{
				throw (ex);
			}
		
		return(strResult);
	}

/*****************************************************************
 * ++
 * Method name:		ReceiveMessage(char*)
 *
 * Description:		Receive message sent from the HOST, first 4 bytes
 *					content total length of the message must be received
 *
 * Global variable:
 *					mSocket
 *					mbSocketOK
 *
 * Return:			Size of the message received
 *
 * Modifications	: QuanLD
 *                                 Function first created
 *                                 SIBS-IBPS-GW 
 *                                 Create new socket
 * --
 ******************************************************************/

long CCMessage::ReceiveMessage(char* szMsgRequest)
{
	long iReturn=0;
	char szMessage[GW_MAX_LENGTH_STRING];                // ASCII string 
	char szLengthConverted[10];
	char szLength[5];
	long lMsgSize=700;
	char szTemp[GW_MAX_LENGTH_STRING];
	fd_set fsRead;
	timeval tmWait = {100,100};

	FD_ZERO(&fsRead);
	FD_SET(mSocket,&fsRead);

	// Check if there is any data received. If there is, display it.
	iReturn = select(0, &fsRead, 0, 0, &tmWait);
	if (iReturn == SOCKET_ERROR)
	{
		//LogError("Connection to the HOST corrupted!!! ", GW_ERROR);
		return(0);
	}
	// Finish receiving
	else if (iReturn == 0)
	{
		return(0);
	}


	// Receive successful, call CMessageTable to process message
	else
	{
		// Read length of the receiving message
		iReturn = recv ((SOCKET)mSocket, szLength, 4, MSG_PEEK );
		if(iReturn==4)
		{
			btoa((unsigned char*) szLengthConverted, 9, (unsigned char *) szLength, 4);
			lMsgSize = atol(szLengthConverted);
		}
		else
		{
			return(0);
		}
		
		// If msg is broken, Get msg with full length, the length now must be lMsgSize
		long lReceived = 0;  // 4 first bytes is not included
		long lCount = 0; 

		// Try to fill up broken message 100 times
		// after that, stop receive and return to Main Service Loop
		while((lReceived-4 < lMsgSize)&&(lCount++ < GW_TIMES_TRY_GET_BROKEN_MSG))
		{
			szMessage[lReceived] = '\0';
			iReturn = select(0, &fsRead, 0, 0, &tmWait);
			if(iReturn>0)
			{
				iReturn = recv ((SOCKET)mSocket, szTemp, lMsgSize - lReceived + 4, 0);
				for(int i=0; i<iReturn; i++)
				{
					szMsgRequest[lReceived+i]=szTemp[i];
				}
				lReceived = lReceived + iReturn;
				if(iReturn == SOCKET_ERROR)
				{
					return(0);
				}
			}
			if(iReturn == SOCKET_ERROR)
			{
				return(0);
			}
			iReturn = lReceived;
		}
	}
    closesocket(mSocket);
	return(iReturn);
}


//
//	///*****************************************************************
//// * ++
//// * Method name:		ReceiveMessage(char*)
// *
// * Description:		Receive message sent from the HOST, first 4 bytes
// *					content total length of the message must be received
// *
// * Global variable:
// *					mSocket
// *					mbSocketOK
// *
// * Return:			Size of the message received
// *
// * Modifications	: QuanLD
// *                                 Function first created
// *                                 SIBS-IBPS-GW 
// *                                 Create new socket
// * --
// ******************************************************************/
	char* CCMessage::ReceiveMessagechar(String^ strReceive) //strMsgRequest)
	{
		char* szMsgRequest =(char*)(void*)Marshal::StringToHGlobalAnsi(strReceive);
		long iReturn=0;
		char szMessage[GW_MAX_LENGTH_STRING];                // ASCII string 
		char szLengthConverted[10];
		char szLength[5];
		long lMsgSize=700;
		char szTemp[GW_MAX_LENGTH_STRING];
		fd_set fsRead;
		timeval tmWait = {100,100};

		FD_ZERO(&fsRead);
		FD_SET(mSocket,&fsRead);

		// Check if there is any data received. If there is, display it.
		iReturn = select(0, &fsRead, 0, 0, &tmWait);
		if (iReturn == SOCKET_ERROR)
		{
			//LogError("Connection to the HOST corrupted!!! ", GW_ERROR);
			return("");
		}
		// Finish receiving
		else if (iReturn == 0)
		{
			return("");
		}


		// Receive successful, call CCMessage to process message
		else
		{
			// Read length of the receiving message
			iReturn = recv ((SOCKET)mSocket, szLength, 4, MSG_PEEK );
			if(iReturn==4)
			{
				btoa((unsigned char*) szLengthConverted, 9, (unsigned char *) szLength, 4);
				lMsgSize = atol(szLengthConverted);
			}
			else
			{
				return("");
			}
			
			// If msg is broken, Get msg with full length, the length now must be lMsgSize
			long lReceived = 0;  // 4 first bytes is not included
			long lCount = 0; 

			// Try to fill up broken message 100 times
			// after that, stop receive and return to Main Service Loop
			
			while((lReceived-4 < lMsgSize)&&(lCount++ < GW_TIMES_TRY_GET_BROKEN_MSG))
			{
				szMessage[lReceived] = '\0';
				iReturn = select(0, &fsRead, 0, 0, &tmWait);
				if(iReturn>0)
				{
					iReturn = recv ((SOCKET)mSocket, szTemp, lMsgSize - lReceived + 4, 0);
					for(int i=0; i<iReturn; i++)
					{
						szMsgRequest[lReceived+i]=szTemp[i];
					}
					lReceived = lReceived + iReturn;
					if(iReturn == SOCKET_ERROR)
					{
						return("");
					}
				}
				if(iReturn == SOCKET_ERROR)
				{
					return("");
				}
				
			}
		}
	
		return(szMsgRequest);
	}

//
//
///*****************************************************************
// * ++
// * Method name:		CreateSocket()
// *
// * Description:		- Create a socket at mSocket
// *					- Connect this socket to port, server in 
// *						mszSIBSServerIPAddress and mulSIBSIPPort
// *					- Try to connect TIMES_TRY_TO_CONNECT times 
// *						before return without connection
// *					- mbSocketOK content status of the socket connection
// *
// * Global variable:
// *		Accessed:	mszSIBSServerIPAddress, 
// *					mulSIBSIPPort
// *		Modified:	mSocket
//					mbSocketOK
// *
// * Return:			None
// *
// * Modifications	: QuanLD
// *                                 Function first created
// *                                 SIBS-IBPS-GW 
// *                                 Create new socket
// * --
// ******************************************************************/

void CCMessage::CreateSocket(char* mszSIBSIPAddress ,unsigned short mulSIBSIPPort)
{
	PHOSTENT		phe;
	long			i = 0;

	try
	{
		while(i++ < GW_TIMES_TRY_TO_CONNECT)
		{
			mSocket = socket(AF_INET,		// Go over TCP/IP
							SOCK_STREAM,	// Socket type
							IPPROTO_TCP);	// Protocol
			if (mSocket == INVALID_SOCKET) 
			{
				lMSG_status=-1; // khong connect duoc vao socket
				return;
			}

			// Fill a SOCKADDR_IN struct with address information:
			phe = gethostbyname(mszSIBSIPAddress);
			SOCKADDR_IN saServer;
			saServer.sin_family = AF_INET;
			memcpy((char FAR *)&(saServer.sin_addr), phe->h_addr, phe->h_length);
			saServer.sin_port = htons(mulSIBSIPPort);

			if (connect(mSocket, (PSOCKADDR) &saServer, sizeof(saServer)) == SOCKET_ERROR)
			{
				closesocket(mSocket);
				mbSocketOK = false;
			}
			else
			{
				mbSocketOK = true;
				break;
			}
		}

		// for debug only
		if(mbSocketOK)
		{
			lMSG_status=1;
		}
		else
		{
			lMSG_status=-1;
		}
	}
	catch(char * ex)
	{
		 throw ex;
	}
	return;
}

/*************************************************************************************
 * ++
 * Method name		: CMessage::ConvertFieldAscii
 *
 * Description		: Convert a string in As400 data type to Ascii
 *
 * Parameter:		: char *szValue, long lFieldLength, 
 *					  long lDataType, long Option
 *					  
 * Global variable	: None
 *		Accessed	: None
 *		Modified	: None
 *
 * Return:			: void
 *
 * Created date		: 2.Jan.03  - QuanLD
 * --
 *************************************************************************************/
	bool CCMessage::ConvertToAS400_Table(String^ mszSIBSIPAddress ,
										unsigned short szSIBSPort, 
										DataTable^ tblConvert )
	{
		//char strReturn[CV_MAX_LENGTH_STRING];
		char strReturn[GW_MAX_LENGTH_STRING+1];
		String^ strMSGOut;
		String^ strOut;
		char* szValue;
//		char* szOUT;
		String^ strConvert;
		long lLenght;
		long lGWpos;
		long lSIBS;
		long lDatatype;
		long lMsgLength=0;
		int i=0;
		
		
		while (i < tblConvert->Rows->Count)
		{
			strConvert = tblConvert->Rows[i]["Content"]->ToString(); 

			if (tblConvert->Rows[i]["Lenght"]->ToString()!="")
				lLenght = Convert::ToInt32(tblConvert->Rows[i]["Lenght"]->ToString()); 
			if (tblConvert->Rows[i]["GWpos"]->ToString()!="")
				lGWpos = Convert::ToInt32(tblConvert->Rows[i]["GWpos"]->ToString()); 
			if (tblConvert->Rows[i]["SIBSpos"]->ToString()!="")
				lSIBS = Convert::ToInt32(tblConvert->Rows[i]["SIBSpos"]->ToString()); 
			if (tblConvert->Rows[i]["DataType"]->ToString()!="")
				lDatatype = Convert::ToInt32(tblConvert->Rows[i]["DataType"]->ToString()); 
			//chuyen kiêu string thành kieu char*
			szValue=(char*)(void*)Marshal::StringToHGlobalAnsi(strConvert);
			
			CCMessage::ConvertField400(szValue,lLenght,lDatatype);
	
			CCMessage::PumpIn(strReturn,szValue,lSIBS,lLenght);
			lMsgLength+=lLenght;
			strMSGOut=strMSGOut+ strOut;
			i=i+1;
			
		}
		
	    char *szSIBSAddress = (char*)(void*)Marshal::StringToHGlobalAnsi(mszSIBSIPAddress);

    	long OutLen=lMsgLength;
		bool isReturn=false;
    
		/*char szFilename[20];
		FILE* pFile;
		sprintf(szFilename, "C:\\Temp\\Test.txt\0", OutLen);
		pFile = fopen(szFilename,"w");
		fwrite(strReturn,1,OutLen,pFile);
		fclose(pFile);*/


		isReturn = CCMessage::SendMessage((char*)strReturn,szSIBSAddress,szSIBSPort, OutLen);
		
		return isReturn; 
		//return CCMessage::SendMessage(mszSIBSIPAddress,szSIBSPort,strMSGOut, OutLen);
			
	}

}






