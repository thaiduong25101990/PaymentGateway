
/*********************************************************************/
/*                                                                   */
/* Program name: packed-ascii.c                                      */
/*                                                                   */
/* Description: ASCII and Packed Decimal conversion                  */
/*                                                                   */
/*  Statement:  Licensed Materials - Property of IBM                 */
/*                                                                   */
/*              CICS SupportPac CA0A                                 */
/*              (C) Copyright IBM Corp. 2000                         */
/*                                                                   */
/*********************************************************************/
#include <Stdafx.h>
#include <stdio.h>
#include <stdlib.h>
#include <memory.h>
#include "pdecimal.h"

/* miscellaneous definitions */
#define POSITIVE   0x0F
#define NEGATIVE   0x0D
#define PLUS_SIGN  '+'
#define MINUS_SIGN '-'


/*********************************************************************/
/*                                                                   */
/* Function name: atop                                               */
/*                                                                   */
/* Description: ASCII to Packed Decimal conversion                   */
/*                                                                   */
/* input  - pointer to target area (char *)                          */
/*          length of target (long)                                  */
/*          pointer to source area (char *)                          */
/*          length of source (long)                                  */
/*                                                                   */
/*********************************************************************/
int atop( unsigned char *pTarget, long TargetLen,
          unsigned char *pSource, long SourceLen )
{
  short Rc = PDEC_OK;                        /* return code */
  long SIndex;                              /* source index */
  long TIndex;                              /* target index */
  unsigned char Sign = PLUS_SIGN;            /* default sign */

  /* validate source and target lengths */

  if( SourceLen < 1 || SourceLen > 32 )
  {
    Rc = PDEC_BADLENGTH_ASCII;
  }
  else
  {
    if( TargetLen < 1 || TargetLen > 16 )
    {
      Rc = PDEC_BADLENGTH_PACKED;
    }
  }

  if( Rc == PDEC_OK )
  {
    /* clear target */
    memset( pTarget, 0, TargetLen + 1);

    /* get packing - last digit out of the way first */
    pTarget[TargetLen - 1] = pSource[SourceLen - 1] << 4;

    if( SourceLen > 1 && TargetLen > 1 )
    {
      SIndex = SourceLen - 2;
      for( TIndex = TargetLen - 2 ; TIndex >= 0 ; TIndex-- )
      {
        if(( SIndex == 0 ) &&
           ( pSource[0] == PLUS_SIGN || pSource[0] == MINUS_SIGN ))
        {
          Sign = pSource[SIndex];            /* save sign */
          break;                             /* force end of loop */
        }
        else
        {
          pTarget[TIndex] = pSource[SIndex] & 0x0f; /* right-hand nibble */
          if( --SIndex >= 0 )
          {
            pTarget[TIndex] |= pSource[SIndex] << 4; /* left-hand nibble */
            if( --SIndex < 0 )
            {
              break;                         /* force end of loop */
            }
          }
        }
      }
    }

    /* put sign on target */
    if( Sign == PLUS_SIGN )
    {
      pTarget[TargetLen - 1] |= POSITIVE;
    }
    else
    {
      pTarget[TargetLen - 1] |= NEGATIVE;
    }
  }

  return( Rc );
}


/*********************************************************************/
/*                                                                   */
/* Function name: ptoa.c                                              */
/*                                                                   */
/* Description: Packed Decimal to ASCII conversion                   */
/*                                                                   */
/* input  - pointer to target area (char *)                          */
/*          length of target (long)                                  */
/*          pointer to source area (char *)                          */
/*          length of source (long)                                  */
/*                                                                   */
/*********************************************************************/

int ptoa( unsigned char *pTarget, long TargetLen,
          unsigned char *pSource, long SourceLen )
{
  short Rc = PDEC_OK;                        /* return code */
  long SIndex;                              /* source index */
  long TIndex;                              /* target index */

  /* validate source and target lengths */

  if( TargetLen < 1 || TargetLen > 32 )
  {
    Rc = PDEC_BADLENGTH_ASCII;
  }
  else
  {
    if( SourceLen < 1 || SourceLen > 16 )
    {
      Rc = PDEC_BADLENGTH_PACKED;
    }
  }

  if( Rc == PDEC_OK )
  {
    /* clear target */
    memset( pTarget, 0, TargetLen +1 );

    /* get unpacking - last digit out of the way first */
    pTarget[TargetLen - 1] = ( pSource[SourceLen - 1] >> 4 ) | 0x30;

    if( SourceLen > 1 && TargetLen > 1 )
    {
      TIndex = TargetLen - 2;
      for( SIndex = SourceLen - 2 ; SIndex >= 0 ; SIndex-- )
      {
        /* Unpack right half of packed decimal byte */
        pTarget[TIndex] = ( pSource[SIndex] & 0x0f ) | 0x30;
        if( --TIndex >= 0 )
        {
          /* Unpack left half of packed decimal byte */
          pTarget[TIndex] = ( pSource[SIndex] >> 4 ) | 0x30;
          if( --TIndex < 0 )
          {
            break;
          }
        }
        else
        {
          break;                             /* force loop exit */
        }
      }

      if(( Rc == PDEC_OK ) &&
         (( pSource[SourceLen - 1] & 0x0f ) == NEGATIVE ))
      {
        Rc = PDEC_RESULT_NEGATIVE;           /* Return negative sign */
      }
    }
  }

  return( Rc );
}

/*********************************************************************/
/*                                                                   */
/* Function name: atos                                               */
/*                                                                   */
/* Description: ASCII to Zoned Decimal conversion                    */
/*                                                                   */
/* input  - pointer to target area (char *)                          */
/*          length of target (long)                                  */
/*          pointer to source area (char *)                          */
/*          length of source (long)                                  */
/*                                                                   */
/*********************************************************************/
int atos( unsigned char *pTarget,
          unsigned char *pSource, long SourceLen)
{
  short Rc = PDEC_OK;                        /* return code */  
  long TIndex;                              /* target index */
  unsigned char Sign = PLUS_SIGN;            /* default sign */

  /* validate source and target lengths */

  if( SourceLen < 1 || SourceLen > 32 )
  {
    Rc = PDEC_BADLENGTH_ASCII;
  }
  
  if( Rc == PDEC_OK )
  {
    /* clear target */
	memset( pTarget, 0, SourceLen + 1);

	for( TIndex = SourceLen - 1; TIndex >= 0 ; TIndex-- )
	{
		if(( TIndex == 0 ) &&
			( pSource[0] == PLUS_SIGN || pSource[0] == MINUS_SIGN ))
		{
			Sign = pSource[TIndex];            /* save sign */
			break;                             /* force end of loop */
		}
		else
		{
			pTarget[TIndex] = pSource[TIndex] & 0x0f; /* right-hand nibble */	
			pTarget[TIndex] |= 0xf0; /* left-hand nibble */
		}		
	}    

    /* put sign on target */
    if( Sign == MINUS_SIGN )
    {
      pTarget[SourceLen - 1] |= 0xC0;
    }    
  }

  return( Rc );
}

/*********************************************************************/
/*                                                                   */
/* Function name: stoa.c                                              */
/*                                                                   */
/* Description: Zoned Decimal to ASCII conversion                   */
/*                                                                   */
/* input  - pointer to target area (char *)                          */
/*          length of target (long)                                  */
/*          pointer to source area (char *)                          */
/*          length of source (long)                                  */
/*                                                                   */
/*********************************************************************/

int stoa( unsigned char *pTarget, 
          unsigned char *pSource, long SourceLen )
{
  short Rc = PDEC_OK;                        /* return code */

  long TIndex;                              /* target index */

  /* validate source and target lengths */

  if( SourceLen < 1 || SourceLen > 32 )
  {
    Rc = PDEC_BADLENGTH_ASCII;
  }
  
	if( Rc == PDEC_OK )
	{
		/* clear target */
		memset( pTarget, 0, SourceLen +1 );

		for( TIndex = SourceLen - 1 ; TIndex >= 0 ; TIndex-- )
		{
			/* Unpack right half of packed decimal byte */
			pTarget[TIndex] = ( pSource[TIndex] & 0x0f ) | 0x30;
			
			if(( Rc == PDEC_OK ) &&
			(( pSource[SourceLen - 1] & 0xF0 ) == 0xF0))
			{
				Rc = PDEC_RESULT_NEGATIVE;           /* Return negative sign */
			}
		}
	}
  
  return( Rc );
}

/*********************************************************************/
/*                                                                   */
/* Function name: atoh                                               */
/*                                                                   */
/* Description: ASCII to Hexa conversion							 */
/*                                                                   */
/* input  - source area (char )				                         */
/*                                                                   */
/*********************************************************************/
unsigned char atoh(unsigned char pSource)
{
	if(pSource < 0x41) //A
	{
		return pSource;
	}
	else
	{
		return pSource - 55;
	}
}

/*********************************************************************/
/*                                                                   */
/* Function name: atoh                                               */
/*                                                                   */
/* Description: ASCII to Hexa conversion							 */
/*                                                                   */
/* input  - source area (char )				                         */
/*                                                                   */
/*********************************************************************/
unsigned char htoa(unsigned char pSource)
{
	if(pSource < 0x0A) //A
	{		
		return pSource + 48;
	}
	else
	{
		return pSource + 55;
	}
}

/*********************************************************************/
/*                                                                   */
/* Function name: atob                                               */
/*                                                                   */
/* Description: ASCII to Binary conversion							 */
/*                                                                   */
/* input  - pointer to target area (char *)                          */
/*          length of target (long)                                  */
/*          pointer to source area (char *)                          */
/*          length of source (long)                                  */
/*                                                                   */
/*********************************************************************/
int atob( unsigned char *pTarget, long TargetLen,
          unsigned char *pSource, long SourceLen )
{
	short Rc = PDEC_OK;                        /* return code */
	short SIndex;                              /* source index */
	short TIndex;                              /* target index */
	unsigned char Sign = PLUS_SIGN;            /* default sign */
	char strFormat[5];
  
	/* validate source and target lengths */

	if( TargetLen < 1 || TargetLen > 32 )
	{
		Rc = PDEC_BADLENGTH_ASCII;
	}    	
	
	//Convert from char to hexa
	sprintf(strFormat,"0%dX", SourceLen);
	strFormat[0] = '%';
	sprintf((char*)pSource,(const char*)strFormat, atoi((const char*)pSource));

	if( Rc == PDEC_OK )
	{
		/* clear target */
		memset( pTarget, 0, TargetLen + 1);    

		SIndex = SourceLen - 1;
		for( TIndex = TargetLen - 1 ; TIndex >= 0 ; TIndex-- )
		{
			pTarget[TIndex] = atoh(pSource[SIndex]) & 0x0f; /* right-hand nibble */
			if( --SIndex >= 0 )
			{	
				//If the pSource[SIndex] is not A,B,C,D,E								
				pTarget[TIndex] |= atoh(pSource[SIndex]) << 4; /* left-hand nibble */				

				if( --SIndex < 0 )
				{
					break;                         /* force end of loop */	
				}

			}
		}
	}

	return( Rc );
}
/*********************************************************************/
/*                                                                   */
/* Function name: btoa.c                                             */
/*                                                                   */
/* Description: Binary to ASCII conversion							 */
/*                                                                   */
/* input  - pointer to target area (char *)                          */
/*          length of target (long)                                  */
/*          pointer to source area (char *)                          */
/*          length of source (long)                                  */
/*                                                                   */
/*********************************************************************/

int btoa( unsigned char *pTarget, long TargetLen,
          unsigned char *pSource, long SourceLen )
{
  short Rc = PDEC_OK;                        /* return code */
  long SIndex;                              /* source index */
  long TIndex;                              /* target index */
  long nValue = 0;  
  char* stopstring;
  char strFormat[5];
	
  //strFormat = (char*)malloc(4);
  //if(strFormat == NULL)
  //  return PDEC_RESULT_NEGATIVE;

  sprintf_s(strFormat,"00%dd", TargetLen);
  strFormat[0] = '%';

  /* validate source and target lengths */

	if( TargetLen < 1 || TargetLen > 32 )
	{
		Rc = PDEC_BADLENGTH_ASCII;
	}


	if( Rc == PDEC_OK )
	{
		/* clear target */
		//memset( pTarget, 0, TargetLen +1 );    
		memset( pTarget, 0x30, TargetLen + 1);
		pTarget[TargetLen] = 0;

		if( TargetLen > 1 )
		{
			TIndex = TargetLen - 1;
			for( SIndex = SourceLen - 1 ; SIndex >= 0 ; SIndex-- )
			{
				/* Unpack right half of packed decimal byte */
				pTarget[TIndex] = htoa(pSource[SIndex] & 0x0f) ;
				if( --TIndex >= 0 )
				{
					/* Unpack left half of packed decimal byte */
					pTarget[TIndex] = htoa(pSource[SIndex] >> 4) ;
					if( --TIndex < 0 )
					{
						break;
					}
				}
				else
				{
					break;                             /* force loop exit */
				}
			}      
		}
		
		
		
		//nValue = strtol("00000270F", &stopstring, 16);
		nValue = strtol((const char*)pTarget, &stopstring, 16);
		
		sprintf((char*)pTarget,strFormat, nValue);			
	}
	
	//free(strFormat);	
	return( Rc );
}