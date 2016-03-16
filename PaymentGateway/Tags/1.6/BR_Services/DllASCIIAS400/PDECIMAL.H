/*        long          TargetLen;                                   */
/*        unsigned char Source[m];    where m is 1 to 32             */
/*        long          SourceLen;                                   */
/*        short         rc;                                          */
/*        rc = atop( Target, TargetLen, Source, SourceLen );         */
/*                                                                   */
/*        If the first character of Source is + or - it will be used */
/*        to set the sign in Target, otherwise Target will be given  */
/*        a positive sign. Source is not checked for valid numerics. */
/*                                                                   */
/* ptoa - packed decimal to ASCII: emulates S/370 UNPK instruction   */
/*                                                                   */
/*        unsigned char Target[n];    where n is 1 - 32              */
/*        long          TargetLen;                                   */
/*        unsigned char Source[m];    where m is 1 to 16             */
/*        long          SourceLen;                                   */
/*        short         rc;                                          */
/*        rc = ptoa( Target, TargetLen, Source, SourceLen );         */
/*                                                                   */
/*        Source is not checked for valid  packed decimal values.    */
/*        Returns PDEC_OK or PDEC_RESULT_NEGATIVE.                   */
/*                                                                   */
/* ptol - packed decimal to long : emulates S/370 CVB instruction    */
/*                                                                   */
/*        long          Target;                                      */
/*        unsigned char Source[8];                                   */
/*        short         rc;                                          */
/*        rc = ptol( &Target, Source );                              */
/*                                                                   */
/*        Source is checked for valid packed decimal values & sign.  */
/*        If Source exceeds the maximum capacity of a long integer   */
/*        ( +2,147,483,647 to - 2,147,483,648 ), PDEC_OVERFLOW is    */
/*        returned, and the contents of Target are unpredictable.    */
/*                                                                   */
/* ltop - long to packed decimal : emulates S/370 CVD instruction    */
/*                                                                   */
/*        long          Source;                                      */
/*        unsigned char Target[8];                                   */
/*        short         rc;                                          */
/*        rc = ltop( Target, Source );                               */
/*                                                                   */
/*********************************************************************/

int atop( unsigned char *, long, unsigned char *, long );
int ptoa( unsigned char *, long, unsigned char *, long );
int btoa( unsigned char *, long, unsigned char *, long );
int atob( unsigned char *, long, unsigned char *, long );
int stoa( unsigned char *, unsigned char *, long );
int atos( unsigned char *, unsigned char *, long );
void    ASCII_to_EBCDIC ( long, unsigned char *) ;
void    EBCDIC_to_ASCII ( long, unsigned char *) ;

/* return codes */

#define PDEC_OK                0
#define PDEC_RESULT_NEGATIVE  -1
#define PDEC_BADLENGTH_PACKED  1
#define PDEC_BADLENGTH_ASCII   3
#define PDEC_BADDATA_PACKED    7
#define PDEC_OVERFLOW          9
