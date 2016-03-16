<%@ Page Language="C#" MasterPageFile="~/Common/MasterPage.Master" AutoEventWireup="true" CodeBehind="WfrmSearchIBPS.aspx.cs" Inherits="BIDVWEB.BIDV_UC.SearchMessage.WfrmSearchIBPS" Title="MSBGW_WEB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width:100%" align="center">
    <br/>
        <table id="Table1" runat="server" width="740px" class="DialogContentSmall">
            <tr>
                <td align="center" colspan="4" class="DialogCaption" style="height:25px">
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="ĐIỆN IBPS" 
                        ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td style="width:15%">
                    <asp:Label ID="Label3" runat="server" Text="Từ ngày"></asp:Label>
                </td>
                <td style="width:35%">                    
                    <asp:TextBox ID="txtFromDate" runat="server" Width="140px" MaxLength="10" 
                         TabIndex="1" ></asp:TextBox>
                    <asp:Image ID="img1" runat="server" ImageUrl="~/Images/insertdate.gif" 
                        TabIndex="2" />
                </td>    
                <td style="width:20%">
                    <asp:Label ID="Label4" runat="server" Text="Số tiền"></asp:Label>
                </td>
                <td style="width:30%">
                    <asp:TextBox ID="txtAmount" runat="server" Width="140px" MaxLength="20" 
                        Height="22px" TabIndex="3" ontextchanged="txtAmount_TextChanged"
                         AutoPostBack="true"></asp:TextBox>
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Đến ngày"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" Width="140px" MaxLength="10" 
                        Height="22px" TabIndex="4"></asp:TextBox>
                    <asp:Image ID="img2" runat="server" ImageUrl="~/Images/insertdate.gif" 
                        TabIndex="5" />
                </td>                   
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Loại tiền"></asp:Label> 
                </td>
                <td>
                    <asp:DropDownList ID="ddlCurrency" runat="server" Width="140px" 
                        Height="16px" TabIndex="6">
                    </asp:DropDownList> 
                </td>             
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label18" runat="server" Text="NH tạo điện"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCreater" runat="server" Width="140px" MaxLength="12" 
                        Height="22px" TabIndex="7" ></asp:TextBox>                
                </td> 
                <td>
                    <asp:Label ID="Label19" runat="server" Text="Cổng PreTad"></asp:Label>                           
                </td>
                <td>
                    <asp:DropDownList ID="ddlPreTad" runat="server" Width="140px" 
                         TabIndex="8" Style="max-height:50px; min-height:50px; 
                        orphans:10; widows:10">
                    </asp:DropDownList>   
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Ngân hàng gửi"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSender" runat="server" Width="140px" MaxLength="12" 
                        Height="22px" TabIndex="7" ></asp:TextBox>                
                </td> 
                <td>
                    <asp:Label ID="Label13" runat="server" Text="Cổng Tad"></asp:Label>                           
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;BK02&quot; (pFromDate in date,<br />
                    pToDate in date,<br />
                    pBranchType in number,<br />
                    pFeeType in number,<br />
                    pBranch in varchar2,<br />
                    pBranch8 in varchar2,
                    <br />
                    pCCYCD in varchar2,<br />
                    RefCurBK02 IN OUT PKG_CR.RefCurType) IS
                    <br />
                    vBranch varchar2(5);<br />
                    vCCYCD varchar2(3);<br />
                    vFromDate varchar2(10);<br />
                    vToDate varchar2(10);<br />
                    <br />
                    strSql varchar2(10000);
                    <br />
                    BEGIN<br />
                    vFromDate := to_char(pFromDate, &#39;YYYYMMDD&#39;);<br />
                    vToDate := to_char(pToDate, &#39;YYYYMMDD&#39;);
                    <br />
                    if upper(trim(pBranch)) = &#39;ALL&#39; or trim(pBranch) is null then<br />
                    vBranch := &#39;%&#39;;<br />
                    else<br />
                    vBranch := LPAD(pBranch,5,&#39;0&#39;);<br />
                    end if;<br />
                    if upper(trim(pCCYCD)) = &#39;ALL&#39; or trim(pCCYCD) is null then<br />
                    vCCYCD := &#39;%&#39;;<br />
                    else<br />
                    vCCYCD := trim(pCCYCD);<br />
                    end if;<br />
                    --XOA BANG TEMP<br />
                    delete from ibps_cal_Fee_temp;<br />
                    --CHI NHANH TAO DIEN<br />
                    IF pBranchType = 1 THEN
                    <br />
                    --LAY DU LIEU TU BANG IBPS_MSG_CONTNET<br />
                    insert into ibps_cal_Fee_temp<br />
                    (MSG_ID,<br />
                    TRANS_CODE,<br />
                    BRANCH8,<br />
                    SOURCE_BRANCH,
                    <br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    CONTENT,<br />
                    CCYCD)<br />
                    select MSG_ID,<br />
                    TRANS_CODE,<br />
                    F07,<br />
                    LPAD(SOURCE_BRANCH,5,&#39;0&#39;) as SOURCE_BRANCH,<br />
                    NVL(AMOUNT, 0) AS AMOUNT,<br />
                    SENDING_TIME,<br />
                    CONTENT,<br />
                    CCYCD<br />
                    from ibps_msg_content<br />
                    where transdate &gt;= vFromDate<br />
                    AND transdate &lt;= vToDate<br />
                    --and CCYCD LIKE DECODE(pCCYCD, &#39;ALL&#39;, &#39;%&#39;, pCCYCD)<br />
                    AND LPAD(SOURCE_BRANCH,5,&#39;0&#39;) like vBranch
                    <br />
                    AND status = 1<br />
                    AND CCYCD like vCCYCD
                    <br />
                    AND DEPARTMENT &lt;&gt; &#39;TR&#39;<br />
                    AND MSG_DIRECTION = &#39;SIBS-IBPS&#39;;<br />
                    --LAY DU LIEU TU BANG IBPS_MSG_ALL<br />
                    insert into ibps_cal_Fee_temp<br />
                    (MSG_ID,<br />
                    TRANS_CODE,<br />
                    BRANCH8,<br />
                    SOURCE_BRANCH,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    CONTENT,<br />
                    CCYCD)<br />
                    select MSG_ID,<br />
                    TRANS_CODE,<br />
                    F07,<br />
                    LPAD(SOURCE_BRANCH,5,&#39;0&#39;) as SOURCE_BRANCH,<br />
                    NVL(AMOUNT, 0) AS AMOUNT,<br />
                    SENDING_TIME,<br />
                    CONTENT,<br />
                    CCYCD<br />
                    from ibps_msg_all<br />
                    where transdate &gt;= vFromDate<br />
                    and transdate &lt;= vToDate<br />
                    --and CCYCD LIKE DECODE(pCCYCD, &#39;ALL&#39;, &#39;%&#39;, pCCYCD)<br />
                    AND LPAD(SOURCE_BRANCH,5,&#39;0&#39;) like vBranch
                    <br />
                    AND status = 1<br />
                    AND CCYCD like vCCYCD
                    <br />
                    AND DEPARTMENT &lt;&gt; &#39;TR&#39;<br />
                    and MSG_DIRECTION = &#39;SIBS-IBPS&#39;;<br />
                    --LAY DU LIEU TU BANG IBPS_MSG_ALL_HIS<br />
                    insert into ibps_cal_Fee_temp<br />
                    (MSG_ID,<br />
                    TRANS_CODE,<br />
                    BRANCH8,<br />
                    SOURCE_BRANCH,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    CONTENT,<br />
                    CCYCD)<br />
                    select MSG_ID,<br />
                    TRANS_CODE,<br />
                    F07,<br />
                    LPAD(SOURCE_BRANCH,5,&#39;0&#39;) as SOURCE_BRANCH,<br />
                    NVL(AMOUNT, 0) AS AMOUNT,<br />
                    SENDING_TIME,<br />
                    CONTENT,<br />
                    CCYCD<br />
                    from ibps_msg_all_his<br />
                    where transdate &gt;= vFromDate<br />
                    and transdate &lt;= vToDate<br />
                    --and CCYCD LIKE DECODE(pCCYCD, &#39;ALL&#39;, &#39;%&#39;, pCCYCD)<br />
                    AND LPAD(SOURCE_BRANCH,5,&#39;0&#39;) like vBranch
                    <br />
                    AND status = 1<br />
                    AND CCYCD like vCCYCD
                    <br />
                    AND DEPARTMENT &lt;&gt; &#39;TR&#39;<br />
                    and MSG_DIRECTION = &#39;SIBS-IBPS&#39;;
                    <br />
                    --CHI GUI DIEN<br />
                    ELSE
                    <br />
                    --LAY DU LIEU TU BANG IBPS_MSG_CONTNET<br />
                    insert into ibps_cal_Fee_temp<br />
                    (MSG_ID,<br />
                    TRANS_CODE,<br />
                    BRANCH8,<br />
                    SOURCE_BRANCH,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    CONTENT,<br />
                    CCYCD)<br />
                    select MSG_ID,<br />
                    TRANS_CODE,<br />
                    F21,<br />
                    LPAD(TAD,5,&#39;0&#39;) as TAD,<br />
                    NVL(AMOUNT, 0) AS AMOUNT,<br />
                    SENDING_TIME,<br />
                    CONTENT,<br />
                    CCYCD<br />
                    from ibps_msg_content<br />
                    where transdate &gt;= vFromDate<br />
                    and transdate &lt;= vToDate<br />
                    --and CCYCD LIKE DECODE(pCCYCD, &#39;ALL&#39;, &#39;%&#39;, pCCYCD)<br />
                    AND LPAD(TAD,5,&#39;0&#39;) like vBranch
                    <br />
                    AND status = 1<br />
                    AND CCYCD like vCCYCD
                    <br />
                    AND DEPARTMENT &lt;&gt; &#39;TR&#39;<br />
                    and MSG_DIRECTION = &#39;SIBS-IBPS&#39;;<br />
                    --LAY DU LIEU TU BANG IBPS_MSG_ALL<br />
                    insert into ibps_cal_Fee_temp<br />
                    (MSG_ID,<br />
                    TRANS_CODE,<br />
                    BRANCH8,<br />
                    SOURCE_BRANCH,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    CONTENT,<br />
                    CCYCD)<br />
                    select MSG_ID,<br />
                    TRANS_CODE,<br />
                    F21,<br />
                    LPAD(TAD,5,&#39;0&#39;) as TAD,<br />
                    NVL(AMOUNT, 0) AS AMOUNT,<br />
                    SENDING_TIME,<br />
                    CONTENT,<br />
                    CCYCD<br />
                    from ibps_msg_all<br />
                    where transdate &gt;= vFromDate<br />
                    and transdate &lt;= vToDate<br />
                    --and CCYCD LIKE DECODE(pCCYCD, &#39;ALL&#39;, &#39;%&#39;, pCCYCD)<br />
                    AND LPAD(TAD,5,&#39;0&#39;) like vBranch
                    <br />
                    AND status = 1<br />
                    AND CCYCD like vCCYCD
                    <br />
                    AND DEPARTMENT &lt;&gt; &#39;TR&#39;<br />
                    and MSG_DIRECTION = &#39;SIBS-IBPS&#39;;<br />
                    --LAY DU LIEU TU BANG IBPS_MSG_ALL_HIS<br />
                    insert into ibps_cal_Fee_temp<br />
                    (MSG_ID,<br />
                    TRANS_CODE,<br />
                    BRANCH8,<br />
                    SOURCE_BRANCH,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    CONTENT,<br />
                    CCYCD)<br />
                    select MSG_ID,<br />
                    TRANS_CODE,<br />
                    F21,<br />
                    LPAD(TAD,5,&#39;0&#39;) as TAD,<br />
                    NVL(AMOUNT, 0) AS AMOUNT,<br />
                    SENDING_TIME,<br />
                    CONTENT,<br />
                    CCYCD<br />
                    from ibps_msg_all_his<br />
                    where transdate &gt;= vFromDate<br />
                    and transdate &lt;= vToDate<br />
                    --and CCYCD LIKE DECODE(pCCYCD, &#39;ALL&#39;, &#39;%&#39;, pCCYCD)<br />
                    AND LPAD(TAD,5,&#39;0&#39;) like vBranch
                    <br />
                    AND status = 1<br />
                    AND CCYCD like vCCYCD
                    <br />
                    AND DEPARTMENT &lt;&gt; &#39;TR&#39;<br />
                    and MSG_DIRECTION = &#39;SIBS-IBPS&#39;;
                    <br />
                    END IF;<br />
                    --select DISTINCT SIBS_BANK_CODE, GW_BANK_CODE,descriptions AS BRAN_NAME
                    <br />
                    --from IBPS_BANK_MAP where substr(gw_bank_code,3,3) = &#39;&#39;302&#39;&#39;<br />
                    --TINH PHI
                    <br />
                    strSql := &#39; SELECT DISTINCT K.SIBS_BANK_CODE,&#39; || pBranchType ||
                    <br />
                    &#39; as BranchType,<br />
                    K.CCYCD,K.SumLV,K.NumLV,K.SumHV,K.NumHV,K.SumDCV,K.NumDCV,K.BRAN_NAME
                    <br />
                    FROM<br />
                    (select U.SumLV,U.NumLV,V.SumHV,V.NumHV,X.BRAN_NAME,<br />
                    X.SIBS_BANK_CODE,Y.SumDCV,Y.NumDCV,<br />
                    (CASE WHEN U.CCYCD IS NOT NULL THEN U.CCYCD
                    <br />
                    WHEN V.CCYCD IS NOT NULL THEN V.CCYCD<br />
                    ELSE Y.CCYCD END) AS CCYCD
                    <br />
                    from<br />
                    (select DISTINCT B.SIBS_BANK_CODE, B.BRAN_NAME
                    <br />
                    from branch B)X<br />
                    LEFT JOIN
                    <br />
                    (select Sum(T.AMOUNT)as SumLV,Count(T.MSG_ID)as NumLV,SOURCE_BRANCH, CCYCD
                    <br />
                    from<br />
                    (<br />
                    select A.MSG_ID,A.TRANS_CODE,decode(B.FIXED_FEE,0,<br />
                    (case when B.RATE_FEE * A.AMOUNT/100&gt;= B.MAX_FEE then B.MAX_FEE<br />
                    when B.RATE_FEE *A.AMOUNT/100&lt;= B.MIN_FEE then B.MIN_FEE<br />
                    else B.RATE_FEE * A.AMOUNT/100 end),B.FIXED_FEE) as AMOUNT,<br />
                    A.SOURCE_BRANCH,A.CCYCD<br />
                    from IBPS_CAL_Fee_TEMP A INNER JOIN IBPS_FEE B ON A.CCYCD= B.CCYCD<br />
                    where B.TRANS_TYPE=&#39;&#39;2&#39;&#39; AND B.FEEDISC_TYPE=&#39;&#39;1&#39;&#39; AND
                    <br />
                    to_char(A.TRANS_DATE,&#39;&#39;HH24MISS&#39;&#39;)&gt;to_char(B.FEEDISC_TIME,&#39;&#39;HH24MISS&#39;&#39;)<br />
                    union<br />
                    select A.MSG_ID,A.TRANS_CODE,decode(B.FIXED_FEE,0,<br />
                    (case when B.RATE_FEE * A.AMOUNT/100&gt;= B.MAX_FEE then B.MAX_FEE<br />
                    when B.RATE_FEE *A.AMOUNT/100&lt;= B.MIN_FEE then B.MIN_FEE<br />
                    else B.RATE_FEE * A.AMOUNT/100 end),B.FIXED_FEE) as AMOUNT,<br />
                    A.SOURCE_BRANCH,A.CCYCD
                    <br />
                    from IBPS_CAL_Fee_TEMP A INNER JOIN IBPS_FEE B ON A.CCYCD= B.CCYCD<br />
                    where B.TRANS_TYPE=&#39;&#39;2&#39;&#39; AND B.FEEDISC_TYPE=&#39;&#39;2&#39;&#39; AND
                    <br />
                    to_char(A.TRANS_DATE,&#39;&#39;HH24MISS&#39;&#39;)&lt;=to_char(B.FEEDISC_TIME, &#39;&#39;HH24MISS&#39;&#39;)<br />
                    )T where T.TRANS_CODE=&#39;&#39;101001&#39;&#39; group by T.SOURCE_BRANCH, T.CCYCD
                    <br />
                    )U
                    <br />
                    ON (LPAD(X.SIBS_BANK_CODE,5,&#39;&#39;0&#39;&#39;)=U.SOURCE_BRANCH)<br />
                    LEFT join<br />
                    (<br />
                    select Sum(T.AMOUNT)as SumHV,Count(T.MSG_ID)as NumHV,SOURCE_BRANCH, CCYCD
                    <br />
                    from<br />
                    (<br />
                    select A.MSG_ID,A.TRANS_CODE,decode(B.FIXED_FEE,0,<br />
                    (case when B.RATE_FEE * A.AMOUNT/100&gt;= B.MAX_FEE then B.MAX_FEE<br />
                    when B.RATE_FEE *A.AMOUNT/100&lt;= B.MIN_FEE then B.MIN_FEE<br />
                    else B.RATE_FEE * A.AMOUNT/100 end),B.FIXED_FEE) as AMOUNT,<br />
                    A.SOURCE_BRANCH,A.CCYCD<br />
                    from IBPS_CAL_Fee_TEMP A INNER JOIN IBPS_FEE B ON A.CCYCD= B.CCYCD<br />
                    where B.TRANS_TYPE=&#39;&#39;1&#39;&#39; AND B.FEEDISC_TYPE=&#39;&#39;1&#39;&#39; AND
                    <br />
                    to_char(A.TRANS_DATE,&#39;&#39;HH24MISS&#39;&#39;)&gt;to_char(B.FEEDISC_TIME,&#39;&#39;HH24MISS&#39;&#39;)<br />
                    union<br />
                    select A.MSG_ID,A.TRANS_CODE,decode(B.FIXED_FEE,0,<br />
                    (case when B.RATE_FEE * A.AMOUNT/100&gt;= B.MAX_FEE then B.MAX_FEE<br />
                    when B.RATE_FEE *A.AMOUNT/100&lt;= B.MIN_FEE then B.MIN_FEE<br />
                    else B.RATE_FEE * A.AMOUNT/100 end),B.FIXED_FEE) as AMOUNT,<br />
                    A.SOURCE_BRANCH,A.CCYCD
                    <br />
                    from IBPS_CAL_Fee_TEMP A INNER JOIN IBPS_FEE B ON A.CCYCD= B.CCYCD<br />
                    where B.TRANS_TYPE=&#39;&#39;1&#39;&#39; AND B.FEEDISC_TYPE=&#39;&#39;2&#39;&#39; AND
                    <br />
                    to_char(A.TRANS_DATE,&#39;&#39;HH24MISS&#39;&#39;)&lt;=to_char(B.FEEDISC_TIME,&#39;&#39;HH24MISS&#39;&#39;)<br />
                    )T where T.TRANS_CODE=&#39;&#39;201001&#39;&#39; group by T.SOURCE_BRANCH, T.CCYCD
                    <br />
                    )V<br />
                    ON (LPAD(X.SIBS_BANK_CODE,5,&#39;&#39;0&#39;&#39;)=V.SOURCE_BRANCH)
                    <br />
                    LEFT join<br />
                    (select Sum(T.AMOUNT)as SumDCV,Count(T.MSG_ID)as NumDCV,SOURCE_BRANCH,CCYCD
                    <br />
                    from<br />
                    (<br />
                    select A.MSG_ID,A.TRANS_CODE,decode(B.FIXED_FEE,0,<br />
                    (case when B.RATE_FEE * A.AMOUNT/100&gt;= B.MAX_FEE then B.MAX_FEE<br />
                    when B.RATE_FEE *A.AMOUNT/100&lt;= B.MIN_FEE then B.MIN_FEE<br />
                    else B.RATE_FEE * A.AMOUNT/100 end),B.FIXED_FEE) as AMOUNT,<br />
                    A.SOURCE_BRANCH,A.CCYCD
                    <br />
                    from IBPS_CAL_Fee_TEMP A INNER JOIN IBPS_FEE B ON A.CCYCD= B.CCYCD
                    <br />
                    where substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(A.CONTENT,<br />
                    &#39;&#39;019&#39;&#39;),3,3) = substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(A.CONTENT,<br />
                    &#39;&#39;022&#39;&#39;),3,3)<br />
                    and substr(GW_PK_IBPS_Q_CONVERTOUT.get_ibps_field(A.CONTENT,<br />
                    &#39;&#39;019&#39;&#39;),3,3)=&#39;&#39;101&#39;&#39; AND B.TRANS_TYPE=&#39;&#39;3&#39;&#39;
                    <br />
                    )T group by T.SOURCE_BRANCH,T.CCYCD)Y
                    <br />
                    ON (LPAD(X.SIBS_BANK_CODE,5,&#39;&#39;0&#39;&#39;)=Y.SOURCE_BRANCH)<br />
                    ) K<br />
                    WHERE NVL(K.NUMHV,0)+ NVL(K.NUMLV,0) + NVL(K.NUMDCV,0) &gt; 0
                    <br />
                    ORDER BY K.SIBS_BANK_CODE, K.CCYCD &#39;;<br />
                    open RefCurBK02 for strSql;
                    <br />
                    END BK02;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;BK02_DETAIL&quot; (pFromDate in date,<br />
                    pToDate in date,<br />
                    pBranchType in number,<br />
                    pFeeType in number,<br />
                    pBranch in varchar2,<br />
                    pBranch8 in varchar2,<br />
                    pCCYCD in varchar2,<br />
                    RefCurBK02 IN OUT PKG_CR.RefCurType) IS<br />
                    vBranch varchar2(5);<br />
                    vCCYCD varchar2(3);<br />
                    vFromDate varchar2(10);<br />
                    vToDate varchar2(10);<br />
                    <br />
                    strSql varchar2(10000);<br />
                    BEGIN<br />
                    vFromDate := to_char(pFromDate, &#39;YYYYMMDD&#39;);<br />
                    vToDate := to_char(pToDate, &#39;YYYYMMDD&#39;);<br />
                    if upper(trim(pBranch)) = &#39;ALL&#39; or trim(pBranch) is null then<br />
                    vBranch := &#39;%&#39;;<br />
                    else<br />
                    vBranch := LPAD(pBranch,5,&#39;0&#39;);<br />
                    end if;<br />
                    if upper(trim(pCCYCD)) = &#39;ALL&#39; or trim(pCCYCD) is null then<br />
                    vCCYCD := &#39;%&#39;;<br />
                    else<br />
                    vCCYCD := trim(pCCYCD);<br />
                    end if;<br />
                    --XOA BANG TEMP<br />
                    delete from ibps_cal_Fee_temp;<br />
                    --CHI NHANH TAO DIEN<br />
                    IF pBranchType = 1 THEN<br />
                    --LAY DU LIEU TU BANG IBPS_MSG_CONTNET<br />
                    insert into ibps_cal_Fee_temp<br />
                    (MSG_ID,<br />
                    TRANS_CODE,<br />
                    BRANCH8,<br />
                    SOURCE_BRANCH,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS)<br />
                    select MSG_ID,<br />
                    TRANS_CODE,<br />
                    F07,<br />
                    LPAD(SOURCE_BRANCH,5,&#39;0&#39;) as SOURCE_BRANCH,<br />
                    NVL(AMOUNT, 0) AS AMOUNT,<br />
                    SENDING_TIME,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS<br />
                    from ibps_msg_content<br />
                    where transdate &gt;= vFromDate<br />
                    AND transdate &lt;= vToDate<br />
                    --and CCYCD LIKE DECODE(pCCYCD, &#39;ALL&#39;, &#39;%&#39;, pCCYCD)<br />
                    AND LPAD(SOURCE_BRANCH,5,&#39;0&#39;) like vBranch<br />
                    AND status = 1<br />
                    AND msg_src &lt;&gt; 2<br />
                    AND CCYCD like vCCYCD<br />
                    AND DEPARTMENT &lt;&gt; &#39;TR&#39;<br />
                    AND MSG_DIRECTION = &#39;SIBS-IBPS&#39;;<br />
                    --LAY DU LIEU TU BANG IBPS_MSG_ALL<br />
                    insert into ibps_cal_Fee_temp<br />
                    (MSG_ID,<br />
                    TRANS_CODE,<br />
                    BRANCH8,<br />
                    SOURCE_BRANCH,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS)<br />
                    select MSG_ID,<br />
                    TRANS_CODE,<br />
                    F07,<br />
                    LPAD(SOURCE_BRANCH,5,&#39;0&#39;) as SOURCE_BRANCH,<br />
                    NVL(AMOUNT, 0) AS AMOUNT,<br />
                    SENDING_TIME,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS<br />
                    from ibps_msg_all<br />
                    where transdate &gt;= vFromDate<br />
                    and transdate &lt;= vToDate<br />
                    --and CCYCD LIKE DECODE(pCCYCD, &#39;ALL&#39;, &#39;%&#39;, pCCYCD)<br />
                    AND LPAD(SOURCE_BRANCH,5,&#39;0&#39;) like vBranch<br />
                    AND status = 1<br />
                    AND msg_src &lt;&gt; 2<br />
                    AND CCYCD like vCCYCD<br />
                    AND DEPARTMENT &lt;&gt; &#39;TR&#39;<br />
                    and MSG_DIRECTION = &#39;SIBS-IBPS&#39;;<br />
                    --LAY DU LIEU TU BANG IBPS_MSG_ALL_HIS<br />
                    insert into ibps_cal_Fee_temp<br />
                    (MSG_ID,<br />
                    TRANS_CODE,<br />
                    BRANCH8,<br />
                    SOURCE_BRANCH,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS)<br />
                    select MSG_ID,<br />
                    TRANS_CODE,<br />
                    F07,<br />
                    LPAD(SOURCE_BRANCH,5,&#39;0&#39;) as SOURCE_BRANCH,<br />
                    NVL(AMOUNT, 0) AS AMOUNT,<br />
                    SENDING_TIME,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS<br />
                    from ibps_msg_all_his<br />
                    where transdate &gt;= vFromDate<br />
                    and transdate &lt;= vToDate<br />
                    --and CCYCD LIKE DECODE(pCCYCD, &#39;ALL&#39;, &#39;%&#39;, pCCYCD)<br />
                    AND LPAD(SOURCE_BRANCH,5,&#39;0&#39;) like vBranch<br />
                    AND status = 1<br />
                    AND msg_src &lt;&gt; 2<br />
                    AND CCYCD like vCCYCD<br />
                    AND DEPARTMENT &lt;&gt; &#39;TR&#39;<br />
                    and MSG_DIRECTION = &#39;SIBS-IBPS&#39;;<br />
                    --CHI GUI DIEN<br />
                    ELSE<br />
                    --LAY DU LIEU TU BANG IBPS_MSG_CONTNET<br />
                    insert into ibps_cal_Fee_temp<br />
                    (MSG_ID,<br />
                    TRANS_CODE,<br />
                    BRANCH8,<br />
                    SOURCE_BRANCH,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS)<br />
                    select MSG_ID,<br />
                    TRANS_CODE,<br />
                    F21,<br />
                    LPAD(TAD,5,&#39;0&#39;) as TAD,<br />
                    NVL(AMOUNT, 0) AS AMOUNT,<br />
                    SENDING_TIME,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS<br />
                    from ibps_msg_content<br />
                    where transdate &gt;= vFromDate<br />
                    and transdate &lt;= vToDate<br />
                    --and CCYCD LIKE DECODE(pCCYCD, &#39;ALL&#39;, &#39;%&#39;, pCCYCD)<br />
                    AND LPAD(TAD,5,&#39;0&#39;) like vBranch<br />
                    AND status = 1<br />
                    AND msg_src &lt;&gt; 2<br />
                    AND CCYCD like vCCYCD<br />
                    AND DEPARTMENT &lt;&gt; &#39;TR&#39;<br />
                    and MSG_DIRECTION = &#39;SIBS-IBPS&#39;;<br />
                    --LAY DU LIEU TU BANG IBPS_MSG_ALL<br />
                    insert into ibps_cal_Fee_temp<br />
                    (MSG_ID,<br />
                    TRANS_CODE,<br />
                    BRANCH8,<br />
                    SOURCE_BRANCH,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS)<br />
                    select MSG_ID,<br />
                    TRANS_CODE,<br />
                    F21,<br />
                    LPAD(TAD,5,&#39;0&#39;) as TAD,<br />
                    NVL(AMOUNT, 0) AS AMOUNT,<br />
                    SENDING_TIME,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS<br />
                    from ibps_msg_all<br />
                    where transdate &gt;= vFromDate<br />
                    and transdate &lt;= vToDate<br />
                    --and CCYCD LIKE DECODE(pCCYCD, &#39;ALL&#39;, &#39;%&#39;, pCCYCD)<br />
                    AND LPAD(TAD,5,&#39;0&#39;) like vBranch<br />
                    AND status = 1<br />
                    AND msg_src &lt;&gt; 2<br />
                    AND CCYCD like vCCYCD<br />
                    AND DEPARTMENT &lt;&gt; &#39;TR&#39;<br />
                    and MSG_DIRECTION = &#39;SIBS-IBPS&#39;;<br />
                    --LAY DU LIEU TU BANG IBPS_MSG_ALL_HIS<br />
                    insert into ibps_cal_Fee_temp<br />
                    (MSG_ID,<br />
                    TRANS_CODE,<br />
                    BRANCH8,<br />
                    SOURCE_BRANCH,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS)<br />
                    select MSG_ID,<br />
                    TRANS_CODE,<br />
                    F21,<br />
                    LPAD(TAD,5,&#39;0&#39;) as TAD,<br />
                    NVL(AMOUNT, 0) AS AMOUNT,<br />
                    SENDING_TIME,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS<br />
                    from ibps_msg_all_his<br />
                    where transdate &gt;= vFromDate<br />
                    and transdate &lt;= vToDate<br />
                    --and CCYCD LIKE DECODE(pCCYCD, &#39;ALL&#39;, &#39;%&#39;, pCCYCD)<br />
                    AND LPAD(TAD,5,&#39;0&#39;) like vBranch<br />
                    AND status = 1<br />
                    AND msg_src &lt;&gt; 2<br />
                    AND CCYCD like vCCYCD<br />
                    AND DEPARTMENT &lt;&gt; &#39;TR&#39;<br />
                    and MSG_DIRECTION = &#39;SIBS-IBPS&#39;;<br />
                    END IF;<br />
                    --select DISTINCT SIBS_BANK_CODE, GW_BANK_CODE,descriptions AS BRAN_NAME<br />
                    --from IBPS_BANK_MAP where substr(gw_bank_code,3,3) = &#39;&#39;302&#39;&#39;<br />
                    --TINH PHI<br />
                    strSql := &#39; select V.MSG_ID,<br />
                    DECODE(V.TRANS_CODE,&#39;&#39;101001&#39;&#39;,&#39;&#39;LV&#39;&#39;,&#39;&#39;HV&#39;&#39;) AS TRANS_CODE,<br />
                    V.FEE,V.AMOUNT,V.SOURCE_BRANCH,<br />
                    V.CCYCD,V.RM_NUMBER,V.GW_TRANS_NUM,Y.NAME AS STATUS,<br />
                    X.BRAN_NAME,X.SIBS_BANK_CODE,&#39; || pBranchType || &#39;<br />
                    as BranchType<br />
                    from<br />
                    (select DISTINCT B.SIBS_BANK_CODE, B.BRAN_NAME<br />
                    from branch B)X<br />
                    RIGHT JOIN<br />
                    ( select A.MSG_ID,A.TRANS_CODE,decode(B.FIXED_FEE,0,<br />
                    (case when B.RATE_FEE * A.AMOUNT/100&gt;= B.MAX_FEE then B.MAX_FEE<br />
                    when B.RATE_FEE *A.AMOUNT/100&lt;= B.MIN_FEE then B.MIN_FEE<br />
                    else B.RATE_FEE * A.AMOUNT/100 end),B.FIXED_FEE) as FEE,<br />
                    A.AMOUNT,A.SOURCE_BRANCH,A.CCYCD,<br />
                    A.RM_NUMBER,A.GW_TRANS_NUM,A.STATUS<br />
                    from IBPS_CAL_Fee_TEMP A INNER JOIN IBPS_FEE B ON A.CCYCD= B.CCYCD<br />
                    where B.TRANS_TYPE=&#39;&#39;2&#39;&#39; AND B.FEEDISC_TYPE=&#39;&#39;1&#39;&#39; AND<br />
                    A.TRANS_CODE=&#39;&#39;101001&#39;&#39; AND<br />
                    to_char(A.TRANS_DATE,&#39;&#39;HH24MISS&#39;&#39;)&gt;to_char(B.FEEDISC_TIME,&#39;&#39;HH24MISS&#39;&#39;)<br />
                    union<br />
                    select A.MSG_ID,A.TRANS_CODE,decode(B.FIXED_FEE,0,<br />
                    (case when B.RATE_FEE * A.AMOUNT/100&gt;= B.MAX_FEE then B.MAX_FEE<br />
                    when B.RATE_FEE *A.AMOUNT/100&lt;= B.MIN_FEE then B.MIN_FEE<br />
                    else B.RATE_FEE * A.AMOUNT/100 end),B.FIXED_FEE) as FEE,<br />
                    A.AMOUNT,A.SOURCE_BRANCH,A.CCYCD,<br />
                    A.RM_NUMBER,A.GW_TRANS_NUM,A.STATUS<br />
                    from IBPS_CAL_Fee_TEMP A INNER JOIN IBPS_FEE B ON A.CCYCD= B.CCYCD<br />
                    where B.TRANS_TYPE=&#39;&#39;2&#39;&#39; AND B.FEEDISC_TYPE=&#39;&#39;2&#39;&#39; AND<br />
                    A.TRANS_CODE=&#39;&#39;101001&#39;&#39; AND<br />
                    to_char(A.TRANS_DATE,&#39;&#39;HH24MISS&#39;&#39;)&lt;=to_char(B.FEEDISC_TIME, &#39;&#39;HH24MISS&#39;&#39;)<br />
                    union<br />
                    select A.MSG_ID,A.TRANS_CODE,decode(B.FIXED_FEE,0,<br />
                    (case when B.RATE_FEE * A.AMOUNT/100&gt;= B.MAX_FEE then B.MAX_FEE<br />
                    when B.RATE_FEE *A.AMOUNT/100&lt;= B.MIN_FEE then B.MIN_FEE<br />
                    else B.RATE_FEE * A.AMOUNT/100 end),B.FIXED_FEE) as FEE,<br />
                    A.AMOUNT,A.SOURCE_BRANCH,A.CCYCD,<br />
                    A.RM_NUMBER,A.GW_TRANS_NUM,A.STATUS<br />
                    from IBPS_CAL_Fee_TEMP A INNER JOIN IBPS_FEE B ON A.CCYCD= B.CCYCD<br />
                    where B.TRANS_TYPE=&#39;&#39;1&#39;&#39; AND B.FEEDISC_TYPE=&#39;&#39;1&#39;&#39; AND<br />
                    A.TRANS_CODE=&#39;&#39;201001&#39;&#39; AND<br />
                    to_char(A.TRANS_DATE,&#39;&#39;HH24MISS&#39;&#39;)&gt;to_char(B.FEEDISC_TIME,&#39;&#39;HH24MISS&#39;&#39;)<br />
                    union<br />
                    select A.MSG_ID,A.TRANS_CODE,decode(B.FIXED_FEE,0,<br />
                    (case when B.RATE_FEE * A.AMOUNT/100&gt;= B.MAX_FEE then B.MAX_FEE<br />
                    when B.RATE_FEE *A.AMOUNT/100&lt;= B.MIN_FEE then B.MIN_FEE<br />
                    else B.RATE_FEE * A.AMOUNT/100 end),B.FIXED_FEE) as FEE,<br />
                    A.AMOUNT,A.SOURCE_BRANCH,A.CCYCD,<br />
                    A.RM_NUMBER,A.GW_TRANS_NUM,A.STATUS<br />
                    from IBPS_CAL_Fee_TEMP A INNER JOIN IBPS_FEE B ON A.CCYCD= B.CCYCD<br />
                    where B.TRANS_TYPE=&#39;&#39;1&#39;&#39; AND B.FEEDISC_TYPE=&#39;&#39;2&#39;&#39; AND<br />
                    A.TRANS_CODE=&#39;&#39;201001&#39;&#39; AND<br />
                    to_char(A.TRANS_DATE,&#39;&#39;HH24MISS&#39;&#39;)&lt;=to_char(B.FEEDISC_TIME,&#39;&#39;HH24MISS&#39;&#39;)<br />
                    )V<br />
                    ON (LPAD(X.SIBS_BANK_CODE,5,&#39;&#39;0&#39;&#39;)=V.SOURCE_BRANCH)<br />
                    INNER JOIN STATUS Y ON V.STATUS = Y.STATUS<br />
                    ORDER BY V.CCYCD,V.SOURCE_BRANCH,V.GW_TRANS_NUM&#39;;<br />
                    open RefCurBK02 for strSql;<br />
                    END BK02_DETAIL;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;BK02_DETAIL_EXCEL&quot; (pFromDate in date,<br />
                    pToDate in date,<br />
                    pBranchType in number,<br />
                    pFeeType in number,<br />
                    pBranch in varchar2,<br />
                    pBranch8 in varchar2,<br />
                    pCCYCD in varchar2,<br />
                    RefCurBK02 IN OUT PKG_CR.RefCurType) IS<br />
                    vBranch varchar2(5);<br />
                    vCCYCD varchar2(3);<br />
                    vFromDate varchar2(10);<br />
                    vToDate varchar2(10);<br />
                    <br />
                    strSql varchar2(10000);<br />
                    BEGIN<br />
                    vFromDate := to_char(pFromDate, &#39;YYYYMMDD&#39;);<br />
                    vToDate := to_char(pToDate, &#39;YYYYMMDD&#39;);<br />
                    if upper(trim(pBranch)) = &#39;ALL&#39; or trim(pBranch) is null then<br />
                    vBranch := &#39;%&#39;;<br />
                    else<br />
                    vBranch := LPAD(pBranch,5,&#39;0&#39;);<br />
                    end if;<br />
                    if upper(trim(pCCYCD)) = &#39;ALL&#39; or trim(pCCYCD) is null then<br />
                    vCCYCD := &#39;%&#39;;<br />
                    else<br />
                    vCCYCD := trim(pCCYCD);<br />
                    end if;<br />
                    --XOA BANG TEMP<br />
                    delete from ibps_cal_Fee_temp;<br />
                    --CHI NHANH TAO DIEN<br />
                    IF pBranchType = 1 THEN<br />
                    --LAY DU LIEU TU BANG IBPS_MSG_CONTNET<br />
                    insert into ibps_cal_Fee_temp<br />
                    (MSG_ID,<br />
                    TRANS_CODE,<br />
                    BRANCH8,<br />
                    SOURCE_BRANCH,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS)<br />
                    select MSG_ID,<br />
                    TRANS_CODE,<br />
                    F07,<br />
                    LPAD(SOURCE_BRANCH,5,&#39;0&#39;) as SOURCE_BRANCH,<br />
                    NVL(AMOUNT, 0) AS AMOUNT,<br />
                    SENDING_TIME,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS<br />
                    from ibps_msg_content<br />
                    where transdate &gt;= vFromDate<br />
                    AND transdate &lt;= vToDate<br />
                    --and CCYCD LIKE DECODE(pCCYCD, &#39;ALL&#39;, &#39;%&#39;, pCCYCD)<br />
                    AND LPAD(SOURCE_BRANCH,5,&#39;0&#39;) like vBranch<br />
                    AND status = 1<br />
                    AND msg_src = 2<br />
                    AND CCYCD like vCCYCD<br />
                    AND DEPARTMENT &lt;&gt; &#39;TR&#39;<br />
                    AND MSG_DIRECTION = &#39;SIBS-IBPS&#39;;<br />
                    --LAY DU LIEU TU BANG IBPS_MSG_ALL<br />
                    insert into ibps_cal_Fee_temp<br />
                    (MSG_ID,<br />
                    TRANS_CODE,<br />
                    BRANCH8,<br />
                    SOURCE_BRANCH,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS)<br />
                    select MSG_ID,<br />
                    TRANS_CODE,<br />
                    F07,<br />
                    LPAD(SOURCE_BRANCH,5,&#39;0&#39;) as SOURCE_BRANCH,<br />
                    NVL(AMOUNT, 0) AS AMOUNT,<br />
                    SENDING_TIME,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS<br />
                    from ibps_msg_all<br />
                    where transdate &gt;= vFromDate<br />
                    and transdate &lt;= vToDate<br />
                    --and CCYCD LIKE DECODE(pCCYCD, &#39;ALL&#39;, &#39;%&#39;, pCCYCD)<br />
                    AND LPAD(SOURCE_BRANCH,5,&#39;0&#39;) like vBranch<br />
                    AND status = 1<br />
                    AND msg_src = 2<br />
                    AND CCYCD like vCCYCD<br />
                    AND DEPARTMENT &lt;&gt; &#39;TR&#39;<br />
                    and MSG_DIRECTION = &#39;SIBS-IBPS&#39;;<br />
                    --LAY DU LIEU TU BANG IBPS_MSG_ALL_HIS<br />
                    insert into ibps_cal_Fee_temp<br />
                    (MSG_ID,<br />
                    TRANS_CODE,<br />
                    BRANCH8,<br />
                    SOURCE_BRANCH,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS)<br />
                    select MSG_ID,<br />
                    TRANS_CODE,<br />
                    F07,<br />
                    LPAD(SOURCE_BRANCH,5,&#39;0&#39;) as SOURCE_BRANCH,<br />
                    NVL(AMOUNT, 0) AS AMOUNT,<br />
                    SENDING_TIME,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS<br />
                    from ibps_msg_all_his<br />
                    where transdate &gt;= vFromDate<br />
                    and transdate &lt;= vToDate<br />
                    --and CCYCD LIKE DECODE(pCCYCD, &#39;ALL&#39;, &#39;%&#39;, pCCYCD)<br />
                    AND LPAD(SOURCE_BRANCH,5,&#39;0&#39;) like vBranch<br />
                    AND status = 1<br />
                    AND msg_src = 2<br />
                    AND CCYCD like vCCYCD<br />
                    AND DEPARTMENT &lt;&gt; &#39;TR&#39;<br />
                    and MSG_DIRECTION = &#39;SIBS-IBPS&#39;;<br />
                    --CHI GUI DIEN<br />
                    ELSE<br />
                    --LAY DU LIEU TU BANG IBPS_MSG_CONTNET<br />
                    insert into ibps_cal_Fee_temp<br />
                    (MSG_ID,<br />
                    TRANS_CODE,<br />
                    BRANCH8,<br />
                    SOURCE_BRANCH,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS)<br />
                    select MSG_ID,<br />
                    TRANS_CODE,<br />
                    F21,<br />
                    LPAD(TAD,5,&#39;0&#39;) as TAD,<br />
                    NVL(AMOUNT, 0) AS AMOUNT,<br />
                    SENDING_TIME,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS<br />
                    from ibps_msg_content<br />
                    where transdate &gt;= vFromDate<br />
                    and transdate &lt;= vToDate<br />
                    --and CCYCD LIKE DECODE(pCCYCD, &#39;ALL&#39;, &#39;%&#39;, pCCYCD)<br />
                    AND LPAD(TAD,5,&#39;0&#39;) like vBranch<br />
                    AND status = 1<br />
                    AND msg_src = 2<br />
                    AND CCYCD like vCCYCD<br />
                    AND DEPARTMENT &lt;&gt; &#39;TR&#39;<br />
                    and MSG_DIRECTION = &#39;SIBS-IBPS&#39;;<br />
                    --LAY DU LIEU TU BANG IBPS_MSG_ALL<br />
                    insert into ibps_cal_Fee_temp<br />
                    (MSG_ID,<br />
                    TRANS_CODE,<br />
                    BRANCH8,<br />
                    SOURCE_BRANCH,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS)<br />
                    select MSG_ID,<br />
                    TRANS_CODE,<br />
                    F21,<br />
                    LPAD(TAD,5,&#39;0&#39;) as TAD,<br />
                    NVL(AMOUNT, 0) AS AMOUNT,<br />
                    SENDING_TIME,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS<br />
                    from ibps_msg_all<br />
                    where transdate &gt;= vFromDate<br />
                    and transdate &lt;= vToDate<br />
                    --and CCYCD LIKE DECODE(pCCYCD, &#39;ALL&#39;, &#39;%&#39;, pCCYCD)<br />
                    AND LPAD(TAD,5,&#39;0&#39;) like vBranch<br />
                    AND status = 1<br />
                    AND msg_src = 2<br />
                    AND CCYCD like vCCYCD<br />
                    AND DEPARTMENT &lt;&gt; &#39;TR&#39;<br />
                    and MSG_DIRECTION = &#39;SIBS-IBPS&#39;;<br />
                    --LAY DU LIEU TU BANG IBPS_MSG_ALL_HIS<br />
                    insert into ibps_cal_Fee_temp<br />
                    (MSG_ID,<br />
                    TRANS_CODE,<br />
                    BRANCH8,<br />
                    SOURCE_BRANCH,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS)<br />
                    select MSG_ID,<br />
                    TRANS_CODE,<br />
                    F21,<br />
                    LPAD(TAD,5,&#39;0&#39;) as TAD,<br />
                    NVL(AMOUNT, 0) AS AMOUNT,<br />
                    SENDING_TIME,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    RM_NUMBER,<br />
                    GW_TRANS_NUM,<br />
                    STATUS<br />
                    from ibps_msg_all_his<br />
                    where transdate &gt;= vFromDate<br />
                    and transdate &lt;= vToDate<br />
                    --and CCYCD LIKE DECODE(pCCYCD, &#39;ALL&#39;, &#39;%&#39;, pCCYCD)<br />
                    AND LPAD(TAD,5,&#39;0&#39;) like vBranch<br />
                    AND status = 1<br />
                    AND msg_src = 2<br />
                    AND CCYCD like vCCYCD<br />
                    AND DEPARTMENT &lt;&gt; &#39;TR&#39;<br />
                    and MSG_DIRECTION = &#39;SIBS-IBPS&#39;;<br />
                    END IF;<br />
                    --select DISTINCT SIBS_BANK_CODE, GW_BANK_CODE,descriptions AS BRAN_NAME<br />
                    --from IBPS_BANK_MAP where substr(gw_bank_code,3,3) = &#39;&#39;302&#39;&#39;<br />
                    --TINH PHI<br />
                    strSql := &#39; select V.MSG_ID,<br />
                    DECODE(V.TRANS_CODE,&#39;&#39;101001&#39;&#39;,&#39;&#39;LV&#39;&#39;,&#39;&#39;HV&#39;&#39;) AS TRANS_CODE,<br />
                    V.FEE,V.AMOUNT,V.SOURCE_BRANCH,<br />
                    V.CCYCD,V.RM_NUMBER,V.GW_TRANS_NUM,Y.NAME AS STATUS,<br />
                    X.BRAN_NAME,X.SIBS_BANK_CODE,&#39; || pBranchType || &#39;<br />
                    as BranchType<br />
                    from<br />
                    (select DISTINCT B.SIBS_BANK_CODE, B.BRAN_NAME<br />
                    from branch B)X<br />
                    RIGHT JOIN<br />
                    ( select A.MSG_ID,A.TRANS_CODE,decode(B.FIXED_FEE,0,<br />
                    (case when B.RATE_FEE * A.AMOUNT/100&gt;= B.MAX_FEE then B.MAX_FEE<br />
                    when B.RATE_FEE *A.AMOUNT/100&lt;= B.MIN_FEE then B.MIN_FEE<br />
                    else B.RATE_FEE * A.AMOUNT/100 end),B.FIXED_FEE) as FEE,<br />
                    A.AMOUNT,A.SOURCE_BRANCH,A.CCYCD,<br />
                    A.RM_NUMBER,A.GW_TRANS_NUM,A.STATUS<br />
                    from IBPS_CAL_Fee_TEMP A INNER JOIN IBPS_FEE B ON A.CCYCD= B.CCYCD<br />
                    where B.TRANS_TYPE=&#39;&#39;2&#39;&#39; AND B.FEEDISC_TYPE=&#39;&#39;1&#39;&#39; AND<br />
                    A.TRANS_CODE=&#39;&#39;101001&#39;&#39; AND<br />
                    to_char(A.TRANS_DATE,&#39;&#39;HH24MISS&#39;&#39;)&gt;to_char(B.FEEDISC_TIME,&#39;&#39;HH24MISS&#39;&#39;)<br />
                    union<br />
                    select A.MSG_ID,A.TRANS_CODE,decode(B.FIXED_FEE,0,<br />
                    (case when B.RATE_FEE * A.AMOUNT/100&gt;= B.MAX_FEE then B.MAX_FEE<br />
                    when B.RATE_FEE *A.AMOUNT/100&lt;= B.MIN_FEE then B.MIN_FEE<br />
                    else B.RATE_FEE * A.AMOUNT/100 end),B.FIXED_FEE) as FEE,<br />
                    A.AMOUNT,A.SOURCE_BRANCH,A.CCYCD,<br />
                    A.RM_NUMBER,A.GW_TRANS_NUM,A.STATUS<br />
                    from IBPS_CAL_Fee_TEMP A INNER JOIN IBPS_FEE B ON A.CCYCD= B.CCYCD<br />
                    where B.TRANS_TYPE=&#39;&#39;2&#39;&#39; AND B.FEEDISC_TYPE=&#39;&#39;2&#39;&#39; AND<br />
                    A.TRANS_CODE=&#39;&#39;101001&#39;&#39; AND<br />
                    to_char(A.TRANS_DATE,&#39;&#39;HH24MISS&#39;&#39;)&lt;=to_char(B.FEEDISC_TIME, &#39;&#39;HH24MISS&#39;&#39;)<br />
                    union<br />
                    select A.MSG_ID,A.TRANS_CODE,decode(B.FIXED_FEE,0,<br />
                    (case when B.RATE_FEE * A.AMOUNT/100&gt;= B.MAX_FEE then B.MAX_FEE<br />
                    when B.RATE_FEE *A.AMOUNT/100&lt;= B.MIN_FEE then B.MIN_FEE<br />
                    else B.RATE_FEE * A.AMOUNT/100 end),B.FIXED_FEE) as FEE,<br />
                    A.AMOUNT,A.SOURCE_BRANCH,A.CCYCD,<br />
                    A.RM_NUMBER,A.GW_TRANS_NUM,A.STATUS<br />
                    from IBPS_CAL_Fee_TEMP A INNER JOIN IBPS_FEE B ON A.CCYCD= B.CCYCD<br />
                    where B.TRANS_TYPE=&#39;&#39;1&#39;&#39; AND B.FEEDISC_TYPE=&#39;&#39;1&#39;&#39; AND<br />
                    A.TRANS_CODE=&#39;&#39;201001&#39;&#39; AND<br />
                    to_char(A.TRANS_DATE,&#39;&#39;HH24MISS&#39;&#39;)&gt;to_char(B.FEEDISC_TIME,&#39;&#39;HH24MISS&#39;&#39;)<br />
                    union<br />
                    select A.MSG_ID,A.TRANS_CODE,decode(B.FIXED_FEE,0,<br />
                    (case when B.RATE_FEE * A.AMOUNT/100&gt;= B.MAX_FEE then B.MAX_FEE<br />
                    when B.RATE_FEE *A.AMOUNT/100&lt;= B.MIN_FEE then B.MIN_FEE<br />
                    else B.RATE_FEE * A.AMOUNT/100 end),B.FIXED_FEE) as FEE,<br />
                    A.AMOUNT,A.SOURCE_BRANCH,A.CCYCD,<br />
                    A.RM_NUMBER,A.GW_TRANS_NUM,A.STATUS<br />
                    from IBPS_CAL_Fee_TEMP A INNER JOIN IBPS_FEE B ON A.CCYCD= B.CCYCD<br />
                    where B.TRANS_TYPE=&#39;&#39;1&#39;&#39; AND B.FEEDISC_TYPE=&#39;&#39;2&#39;&#39; AND<br />
                    A.TRANS_CODE=&#39;&#39;201001&#39;&#39; AND<br />
                    to_char(A.TRANS_DATE,&#39;&#39;HH24MISS&#39;&#39;)&lt;=to_char(B.FEEDISC_TIME,&#39;&#39;HH24MISS&#39;&#39;)<br />
                    )V<br />
                    ON (LPAD(X.SIBS_BANK_CODE,5,&#39;&#39;0&#39;&#39;)=V.SOURCE_BRANCH)<br />
                    INNER JOIN STATUS Y ON V.STATUS = Y.STATUS<br />
                    ORDER BY V.CCYCD,V.SOURCE_BRANCH,V.GW_TRANS_NUM&#39;;<br />
                    open RefCurBK02 for strSql;<br />
                    END BK02_DETAIL_EXCEL;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;BM_IBPS02&quot; (pDate in date,<br />
                    pCitad in varchar2,<br />
                    pCcycd in varchar2 default &#39;VND&#39;,<br />
                    pStatus in varchar2,<br />
                    RefCurBM_IBPS02 IN OUT PKG_CR.RefCurType) IS<br />
                    --vDate date;<br />
                    vCitad varchar2(50);<br />
                    vCcycd varchar2(3);<br />
                    vStatus varchar2(50);<br />
                    vOndate date;<br />
                    BEGIN<br />
                    <br />
                    select sysdate into vOndate from dual;<br />
                    if upper(trim(pCitad)) = &#39;ALL&#39; then<br />
                    vCitad := &#39;%&#39;;<br />
                    else<br />
                    vCitad := trim(lpad(pCitad, 5, &#39;0&#39;));<br />
                    end if;<br />
                    if upper(trim(pCcycd)) = &#39;ALL&#39; then<br />
                    vCcycd := &#39;%&#39;;<br />
                    else<br />
                    vCcycd := trim(pCcycd);<br />
                    end if;<br />
                    if upper(trim(pStatus)) = &#39;ALL&#39; then<br />
                    vStatus := &#39;%&#39;;<br />
                    else<br />
                    vStatus := trim(pStatus);<br />
                    end if;<br />
                    <br />
                    if to_date(pDate, &#39;DD/MM/YYYY&#39;) = to_date(vOndate, &#39;DD/MM/YYYY&#39;) then<br />
                    --open RefCurBM_IBPS02 for<br />
                    delete from BM_IBPS02_TEMP;<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    TRAN_CODE)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND C.ccycd LIKE vCcycd<br />
                    AND C.TAD like vCitad) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    WHERE B.CONTENT LIKE vStatus<br />
                    order by A.Trans_Code,RECEIVER asc;<br />
                    elsif substr(to_char(to_date(pDate, &#39;DD/MM/YYYY&#39;), &#39;DD/MM/YYYY&#39;), 4, 2) =<br />
                    substr(to_char(to_date(vOndate, &#39;DD/MM/YYYY&#39;), &#39;DD/MM/YYYY&#39;),<br />
                    4,<br />
                    2) then<br />
                    delete from BM_IBPS02_TEMP;<br />
                    --Lay du lieu trong ngay<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND C.ccycd LIKE vCcycd<br />
                    AND C.TAD like vCitad) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    WHERE B.CONTENT LIKE vStatus<br />
                    order by A.Trans_Code,RECEIVER asc;<br />
                    --Lay du lieu trong thang<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code<br />
                    FROM IBPS_MSG_ALL C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND C.ccycd LIKE vCcycd<br />
                    AND C.TAD like vCitad) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    WHERE B.CONTENT LIKE vStatus<br />
                    order by A.Trans_Code,RECEIVER asc;<br />
                    --order by tad,RM_NUMBER asc;<br />
                    else<br />
                    delete from BM_IBPS02_TEMP;<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND C.ccycd LIKE vCcycd<br />
                    AND C.TAD like vCitad) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    WHERE B.CONTENT LIKE vStatus<br />
                    order by A.Trans_Code,RECEIVER asc;<br />
                    --order by tad,RM_NUMBER asc;<br />
                    --Lay du lieu trong thang<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code<br />
                    FROM IBPS_MSG_ALL C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND C.ccycd LIKE vCcycd<br />
                    AND C.TAD like vCitad) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    WHERE B.CONTENT LIKE vStatus<br />
                    order by A.Trans_Code,RECEIVER asc;<br />
                    --Lay du lieu trong nam<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code<br />
                    FROM IBPS_MSG_ALL_HIS C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND C.ccycd LIKE vCcycd<br />
                    AND C.TAD like vCitad) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    WHERE B.CONTENT LIKE vStatus<br />
                    order by A.Trans_Code,RECEIVER asc;<br />
                    end if;<br />
                    open RefCurBM_IBPS02 for<br />
                    select BM_IBPS02_TEMP.*,1213 as HRL<br />
                    from BM_IBPS02_TEMP order by GW_TRANS_NUM, RM_NUMBER asc;<br />
                    END BM_IBPS02;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;BM_IBPS13&quot; (pdate IN date,<br />
                    pCcycd in varchar2 default &#39;VND&#39;,<br />
                    pCitad in varchar2,<br />
                    RefCurBM_IBPS13 IN OUT PKG_CR.RefCurType) IS<br />
                    vCitad varchar2(50);<br />
                    vCcycd varchar2(3);<br />
                    vBranchName varchar2(200);<br />
                    BEGIN<br />
                    if upper(trim(pCitad)) = &#39;ALL&#39; or trim(pCitad) is null then<br />
                    vCitad := &#39;%&#39;;<br />
                    vBranchName:=&#39;ALL&#39;;<br />
                    else<br />
                    -- lay ma gia 3 so cong tad de so sanh dieu kien
                    <br />
                    select lpad(T.Sibs_Code,5,&#39;0&#39;) into vCitad from tad T where 
                    T.Gw_Bank_Code=pCitad and rownum=1;<br />
                    --vCitad := trim(pCitad);<br />
                    select substr(T.Sibs_Code,-3) || &#39; - &#39; || T.TAD_NAME into vBranchName from TAD T 
                    WHERE T.GW_BANK_CODE = pCitad<br />
                    and rownum=1;<br />
                    end if;<br />
                    if upper(trim(pCcycd)) = &#39;ALL&#39; or trim(pCcycd) is null then<br />
                    vCcycd := &#39;%&#39;;<br />
                    else<br />
                    vCcycd := trim(pCcycd);<br />
                    end if;<br />
                    <br />
                    open RefCurBM_IBPS13 for<br />
                    select * from (<br />
                    SELECT A.msg_id,<br />
                    A.rm_number,<br />
                    A.sender,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    A.receiver,<br />
                    BRECV.BANK_NAME AS RECEINAME,<br />
                    A.amount,<br />
                    A.ccy,<br />
                    A.trans_date,<br />
                    A.exception_type,<br />
                    A.msg_direction,<br />
                    B.Content AS Status,<br />
                    --decode(C.Query_Id, NULL,decode(CA.Query_Id,NULL, CH.F21,CA.F21),C.F21 ) AS 
                    TAD,<br />
                    decode(trim(A.Tad),NULL,&#39; &#39;,to_char(to_number(A.tad))) AS TAD,<br />
                    --substr(A.tad, -3) AS TAD,<br />
                    T.GW_BANK_CODE ||&#39; - &#39;|| T.TAD_NAME AS TADNAME,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(<br />
                    decode(C.Query_Id, NULL,decode(CA.Query_Id,NULL, 
                    CH.Content,CA.Content),C.Content),<br />
                    &#39;011&#39;) AS SOGD,
                    <br />
                    --decode(C.Query_Id, NULL,decode(CA.Query_Id,NULL, 
                    CH.Gw_Trans_Num,CA.Gw_Trans_Num),C.GW_TRANS_NUM) AS GW_TRANS_NUM,<br />
                    to_number(substr(A.K1,-8)) AS GW_TRANS_NUM,<br />
                    decode(C.Query_Id, NULL,decode(CA.Query_Id,NULL, 
                    CH.Trans_Code,CA.Trans_Code),C.Trans_Code) AS TRANS_CODE,<br />
                    decode(C.Query_Id, NULL,decode(CA.Query_Id,NULL, 
                    CH.SOURCE_BRANCH,CA.SOURCE_BRANCH),C.SOURCE_BRANCH) AS SOURCE_BRANCH,<br />
                    vBranchName AS SRC_BRANCHNAME,<br />
                    decode(C.Query_Id, NULL,decode(CA.Query_Id,NULL, 
                    CH.MSG_SRC,CA.MSG_SRC),C.MSG_SRC) AS MSG_SRC
                    <br />
                    FROM IBPS_MSG_REC A<br />
                    LEFT JOIN IBPS_MSG_CONTENT C ON A.Query_Id = C.Query_Id<br />
                    LEFT JOIN IBPS_MSG_ALL CA ON CA.Query_Id=A.Query_Id<br />
                    LEFT JOIN IBPS_MSG_ALL_HIS CH ON A.Query_Id=CH.Query_Id<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)
                    <br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(C.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(C.Source_Branch,5,&#39;0&#39;))
                    <br />
                    LEFT JOIN TAD T ON lpad(A.TAD,5,&#39;0&#39;) = lpad(T.Sibs_Code,5,&#39;0&#39;)
                    <br />
                    where A.Ndate=to_char(pDate,&#39;YYYYMMDD&#39;)<br />
                    --to_char(A.Trans_Date,&#39;YYYYMMDD&#39;)=to_char(pDate,&#39;YYYYMMDD&#39;)<br />
                    AND decode(trim(A.tad), NULL,&#39;%&#39;, lpad(A.Tad,5,&#39;0&#39;)) like vCitad<br />
                    --AND decode(C.Query_Id, NULL,decode(CA.Query_Id,NULL, 
                    decode(CH.F21,NULL,&#39;%&#39;,CH.F21),CA.F21),C.F21 ) LIKE &#39;%&#39;<br />
                    AND nvl(A.CCY,&#39;%&#39;) LIKE vCcycd<br />
                    ) R<br />
                    where R.GW_TRANS_NUM like &#39;6%&#39; and length(R.GW_TRANS_NUM)=6<br />
                    ;<br />
                    /*LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )*/<br />
                    END BM_IBPS13;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;DELETE_LOG&quot; is<br />
                    begin<br />
                    for i in 1 .. 100 loop<br />
                    begin<br />
                    delete ibps_msg_log<br />
                    where query_id = 0<br />
                    and rownum &lt; 100000;<br />
                    commit;<br />
                    <br />
                    end;<br />
                    end loop;<br />
                    end Delete_Log;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;DELETE_RECONCILE_HUY&quot; is<br />
                    pragma autonomous_transaction;<br />
                    begin<br />
                    Delete from Reconcile_Huy<br />
                    where to_char(transdate, &#39;YYYYMMDD&#39;) = to_char(sysdate, &#39;YYYYMMDD&#39;);<br />
                    commit;<br />
                    end Delete_Reconcile_Huy;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;IBPS_PRINT_MSG_LIST&quot; (pBranch in varchar2,<br />
                    pMSG_ID in number,<br />
                    pUser in varchar2,<br />
                    pcurBM_IBPS_MSG IN OUT PKG_CR.RefCurType) IS<br />
                    /* Dien IBPS*/
                    <br />
                    icount integer;<br />
                    vUser varchar(30);<br />
                    --v_sql varchar2(4000);<br />
                    -- pTblTable varchar2(100);
                    <br />
                    vContent varchar2(4000) := &#39;&#39;;<br />
                    vBranchName varchar2(50) := &#39;&#39;;<br />
                    Msg_ID number(20);<br />
                    rm_number varchar2(20);<br />
                    TransCode varchar2(20);<br />
                    MSG_DIRECTION varchar2(10);<br />
                    Source_Branch varchar2(20);<br />
                    --Source_BranchName varchar2(150);<br />
                    Receive_Branch varchar2(50);<br />
                    --Receive_BranchName varchar2(150);<br />
                    Trans_Date date;<br />
                    Receiving_Time date;<br />
                    Sending_Time date;<br />
                    Pre_Tad varchar2(50);<br />
                    --Pre_TadName varchar2(150);<br />
                    Print_Sts number(1);<br />
                    Gw_Trans_Num number(6);<br />
                    Sender varchar2(30);<br />
                    --SenderName varchar2(150);<br />
                    Receiver varchar2(30);<br />
                    --ReceiverName varchar2(150);<br />
                    AtBankSend varchar2(30);<br />
                    AtBankSendName varchar2(130);<br />
                    AtBankRecei varchar2(30);<br />
                    AtBankReceiName varchar2(130);<br />
                    STATUS varchar2(20);<br />
                    Amount number(19, 2);<br />
                    Descriptions varchar2(512);<br />
                    Tellerid varchar2(30);
                    <br />
                    TellerName varchar(150);<br />
                    vCCYName varchar2(100);
                    <br />
                    vCCYCD varchar2(5);
                    <br />
                    vSibs_TellerID varchar2(30);<br />
                    vF07 varchar2(30);<br />
                    vF19 varchar2(30);<br />
                    vF21 varchar2(30);<br />
                    vF22 varchar2(30);<br />
                    vProductType varchar2(5);<br />
                    BEGIN<br />
                    <br />
                    select count(1)<br />
                    into icount<br />
                    from IBPS_MSG_CONTENT IMC<br />
                    where IMC.MSG_ID = pMSG_ID;<br />
                    <br />
                    if icount = 0 then<br />
                    select count(1)<br />
                    into icount<br />
                    from Ibps_Msg_All IMC<br />
                    where IMC.MSG_ID = pMSG_ID;<br />
                    if icount = 0 then<br />
                    select count(1)<br />
                    into icount<br />
                    from Ibps_Msg_All_His IMC<br />
                    where IMC.MSG_ID = pMSG_ID;<br />
                    if icount &lt;&gt; 0 then<br />
                    -- pTblTable := &#39;Ibps_Msg_All_His&#39;;<br />
                    select BR.Bran_Name<br />
                    into vBranchName<br />
                    From Branch BR<br />
                    where to_number(Br.Sibs_Bank_Code) = to_number(pBranch)<br />
                    and rownum = 1;<br />
                    if vBranchName is null then<br />
                    vBranchName := &#39;&#39;;<br />
                    end if;<br />
                    vUser := pUser;<br />
                    select C.content,<br />
                    C.Msg_id,<br />
                    C.Rm_Number,<br />
                    C.Trans_Code,<br />
                    C.Msg_Direction,<br />
                    C.Source_Branch,<br />
                    C.tad,<br />
                    C.Trans_Date,<br />
                    C.Receiving_Time,<br />
                    C.Sending_Time,<br />
                    C.Pre_Tad,<br />
                    C.Print_Sts,<br />
                    C.Gw_Trans_Num,<br />
                    C.F21 AS SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.F07, -- at send bank<br />
                    C.F22, -- at recei bank<br />
                    S.name, --status<br />
                    C.Amount,<br />
                    C.Trans_Description,<br />
                    vUser,<br />
                    C.Ccycd,<br />
                    C.Sibs_Tellerid,<br />
                    C.F07,<br />
                    C.F19,<br />
                    C.F21,<br />
                    C.F22,<br />
                    C.Product_Type<br />
                    -- C.Tellerid,<br />
                    into vContent,<br />
                    Msg_ID,<br />
                    rm_number,<br />
                    TransCode,<br />
                    MSG_DIRECTION,<br />
                    Source_Branch,<br />
                    Receive_Branch,<br />
                    Trans_Date,<br />
                    Receiving_Time,<br />
                    Sending_Time,<br />
                    Pre_Tad,<br />
                    Print_Sts,<br />
                    Gw_Trans_Num,<br />
                    Sender,<br />
                    Receiver,<br />
                    AtBankSend,<br />
                    AtBankRecei,<br />
                    STATUS,<br />
                    Amount,<br />
                    Descriptions,<br />
                    Tellerid,<br />
                    vCCYCD,<br />
                    vSibs_TellerID,<br />
                    vF07,<br />
                    vF19,<br />
                    vF21,<br />
                    vF22,<br />
                    vProductType<br />
                    from Ibps_Msg_All_His C<br />
                    LEFT JOIN STATUS S ON C.STATUS = S.STATUS<br />
                    where C.msg_id = pMSG_ID;<br />
                    <br />
                    end if;<br />
                    else<br />
                    -- pTblTable := &#39;Ibps_Msg_All&#39;;<br />
                    select BR.Bran_Name<br />
                    into vBranchName<br />
                    From Branch BR<br />
                    where to_number(Br.Sibs_Bank_Code) = to_number(pBranch)<br />
                    and rownum = 1;<br />
                    if vBranchName is null then<br />
                    vBranchName := &#39;&#39;;<br />
                    end if;<br />
                    vUser := pUser;<br />
                    select C.content,<br />
                    C.Msg_id,<br />
                    C.Rm_Number,<br />
                    C.Trans_Code,<br />
                    C.Msg_Direction,<br />
                    C.Source_Branch,<br />
                    C.tad,<br />
                    C.Trans_Date,<br />
                    C.Receiving_Time,<br />
                    C.Sending_Time,<br />
                    C.Pre_Tad,<br />
                    C.Print_Sts,<br />
                    C.Gw_Trans_Num,<br />
                    C.F21 AS SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.F07, -- at send bank<br />
                    C.F22, -- at recei bank<br />
                    S.name, --status<br />
                    C.Amount,<br />
                    C.Trans_Description,<br />
                    vUser,<br />
                    C.Ccycd,<br />
                    C.Sibs_Tellerid,<br />
                    C.F07,<br />
                    C.F19,<br />
                    C.F21,<br />
                    C.F22,<br />
                    C.Product_Type<br />
                    into vContent,<br />
                    Msg_ID,<br />
                    rm_number,<br />
                    TransCode,<br />
                    MSG_DIRECTION,<br />
                    Source_Branch,<br />
                    Receive_Branch,<br />
                    Trans_Date,<br />
                    Receiving_Time,<br />
                    Sending_Time,<br />
                    Pre_Tad,<br />
                    Print_Sts,<br />
                    Gw_Trans_Num,<br />
                    Sender,<br />
                    Receiver,<br />
                    AtBankSend,<br />
                    AtBankRecei,<br />
                    STATUS,<br />
                    Amount,<br />
                    Descriptions,<br />
                    Tellerid,<br />
                    vCCYCD,<br />
                    vSibs_TellerID,<br />
                    vF07,<br />
                    vF19,<br />
                    vF21,<br />
                    vF22,<br />
                    vProductType<br />
                    from Ibps_Msg_All C<br />
                    LEFT JOIN STATUS S ON C.STATUS = S.STATUS<br />
                    where C.msg_id = pMSG_ID;<br />
                    <br />
                    end if;<br />
                    else<br />
                    --pTblTable := &#39;IBPS_MSG_CONTENT&#39;;
                    <br />
                    select BR.Bran_Name<br />
                    into vBranchName<br />
                    From Branch BR<br />
                    where to_number(Br.Sibs_Bank_Code) = to_number(pBranch)<br />
                    and rownum = 1;<br />
                    if vBranchName is null then<br />
                    vBranchName := &#39;&#39;;<br />
                    end if;<br />
                    vUser := pUser;<br />
                    select C.content,<br />
                    C.Msg_id,<br />
                    C.Rm_Number,<br />
                    C.Trans_Code,<br />
                    C.Msg_Direction,<br />
                    C.Source_Branch,<br />
                    C.tad,<br />
                    C.Trans_Date,<br />
                    C.Receiving_Time,<br />
                    C.Sending_Time,<br />
                    C.Pre_Tad,<br />
                    C.Print_Sts,<br />
                    C.Gw_Trans_Num,<br />
                    /* C.F07 AS SENDER,<br />
                    C.F19 AS RECEIVER,<br />
                    C.F21, -- at send bank<br />
                    C.F22, -- at recei bank*/<br />
                    C.F21 AS SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.F07, -- at send bank<br />
                    C.F22, -- at recei bank<br />
                    S.name, --status<br />
                    C.Amount,<br />
                    C.Trans_Description,<br />
                    vUser,<br />
                    C.Ccycd,<br />
                    C.Sibs_Tellerid,<br />
                    C.F07,<br />
                    C.F19,<br />
                    C.F21,<br />
                    C.F22,<br />
                    C.Product_Type<br />
                    into vContent,<br />
                    Msg_ID,<br />
                    rm_number,<br />
                    TransCode,<br />
                    MSG_DIRECTION,<br />
                    Source_Branch,<br />
                    Receive_Branch,<br />
                    Trans_Date,<br />
                    Receiving_Time,<br />
                    Sending_Time,<br />
                    Pre_Tad,<br />
                    Print_Sts,<br />
                    Gw_Trans_Num,<br />
                    Sender,<br />
                    Receiver,<br />
                    AtBankSend,<br />
                    AtBankRecei,<br />
                    STATUS,<br />
                    Amount,<br />
                    Descriptions,<br />
                    Tellerid,<br />
                    vCCYCD,<br />
                    vSibs_TellerID,<br />
                    vF07,<br />
                    vF19,<br />
                    vF21,<br />
                    vF22,<br />
                    vProductType<br />
                    from IBPS_MSG_CONTENT C<br />
                    LEFT JOIN STATUS S ON C.STATUS = S.STATUS<br />
                    where C.msg_id = pMSG_ID;<br />
                    <br />
                    end if;<br />
                    <br />
                    Open pcurBM_IBPS_MSG for<br />
                    select vBranchName as branchname,<br />
                    Msg_ID AS MSG_ID,<br />
                    to_number(rm_number) AS RM_NUMBER,<br />
                    MSG_DIRECTION AS MSG_DIRECTION,<br />
                    decode(trim(vProductType),&#39;OL3&#39;,Receive_Branch, Source_Branch) AS SRCBRANCH,<br />
                    NVL((select substr(Br.Sibs_Bank_Code,-3) || &#39;-&#39; || BR.Bank_Name<br />
                    From IBPS_BANK_MAP BR<br />
                    where Br.Gw_Bank_Code = decode(trim(vProductType),&#39;OL3&#39;,vF21, vF07 )<br />
                    AND 
                    lpad(Br.Sibs_Bank_Code,5,&#39;0&#39;)=lpad(decode(trim(vProductTYpe),&#39;OL3&#39;,Receive_Branch, 
                    Source_Branch),5,&#39;0&#39;)<br />
                    ),<br />
                    &#39;&#39;) AS SRCBRANCHNAME,<br />
                    <br />
                    Receive_Branch AS RECEIBRANCH,<br />
                    NVL((select (Br.Gw_Bank_Code) || &#39;-&#39; || BR.TAD_NAME<br />
                    From TAD BR<br />
                    where Br.Gw_Bank_Code = vF21
                    <br />
                    --AND lpad(Br.Sibs_Bank_Code,5,&#39;0&#39;)=lpad(Receive_Branch,5,&#39;0&#39;)
                    <br />
                    ),<br />
                    &#39;&#39;) AS RECEIBRANCHNAME,<br />
                    <br />
                    Trans_Date AS TRANSDATE,<br />
                    Receiving_Time AS Receiving_Time,<br />
                    Sending_Time AS SEnding_Time,<br />
                    <br />
                    Pre_Tad AS PreTAD,<br />
                    NVL((select (Br.Gw_Bank_Code) || &#39;-&#39; || BR.TAD_NAME<br />
                    From TAD BR<br />
                    where to_number(Br.Sibs_Bank_Code) = to_number(Pre_Tad)),<br />
                    &#39;&#39;) AS PreTADName,<br />
                    <br />
                    Print_Sts AS PRINT_STATUS,<br />
                    Gw_Trans_Num AS BUTTOAN,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, &#39;011&#39;) as SOGD,<br />
                    Sender AS Sender,<br />
                    (select B.bank_name<br />
                    from IBPS_BANK_MAP B<br />
                    where B.gw_bank_code = Sender<br />
                    and rownum = 1) AS BankSenderName,<br />
                    <br />
                    Receiver AS Recerver,<br />
                    (select B.bank_name<br />
                    from IBPS_BANK_MAP B<br />
                    where B.gw_bank_code = Receiver<br />
                    and rownum = 1) AS BankReceiName,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, &#39;028&#39;) as sendname,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, &#39;029&#39;) as sendaddress,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, &#39;030&#39;) as sendacc,<br />
                    <br />
                    AtBankSend AS AtBankSend,<br />
                    (select B.bank_name<br />
                    from IBPS_BANK_MAP B<br />
                    where B.gw_bank_code = AtBankSend<br />
                    and rownum = 1) AS AtBankSendName,<br />
                    <br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, &#39;031&#39;) as receiname,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, &#39;032&#39;) as receiaddress,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, &#39;033&#39;) as receiacc,<br />
                    <br />
                    AtBankRecei AS AtBankRecei,<br />
                    (select B.bank_name<br />
                    from IBPS_BANK_MAP B<br />
                    where B.gw_bank_code = AtBankRecei<br />
                    and rownum = 1) AS AtBankReceiName,<br />
                    trim(STATUS) AS STATUS,<br />
                    Amount AS AMOUNT,<br />
                    vCCYCD as CCY,<br />
                    <br />
                    --GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, &#39;034&#39;) as NOIDUNG,<br />
                    Descriptions as NOIDUNG,<br />
                    Descriptions AS Descriptions,<br />
                    vSibs_TellerID AS TellerID,<br />
                    vUser AS TellerName,<br />
                    TransCode AS TRANS_CODE,<br />
                    (Select CV.V_READ from ccytovie CV where CV.CCYCODE LIKE vCCYCD and rownum=1) AS 
                    CCYNAME,<br />
                    &#39;1&#39; AS GRPID<br />
                    from dual<br />
                    union<br />
                    select vBranchName AS BRANCHNAME,<br />
                    Msg_ID AS MSG_ID,<br />
                    to_number(rm_number) AS RM_NUMBER,<br />
                    MSG_DIRECTION AS MSG_DIRECTION,<br />
                    decode(trim(vProductTYpe),&#39;OL3&#39;,Receive_Branch, Source_Branch) AS SRCBRANCH,<br />
                    NVL((select substr(Br.Sibs_Bank_Code,-3) || &#39;-&#39; || BR.Bank_Name<br />
                    From IBPS_BANK_MAP BR<br />
                    where Br.Gw_Bank_Code = decode(trim(vProductType),&#39;OL3&#39;,vF21, vF07 )<br />
                    AND 
                    lpad(Br.Sibs_Bank_Code,5,&#39;0&#39;)=lpad(decode(trim(vProductTYpe),&#39;OL3&#39;,Receive_Branch, 
                    Source_Branch),5,&#39;0&#39;)<br />
                    ),<br />
                    &#39;&#39;) AS SRCBRANCHNAME,<br />
                    <br />
                    Receive_Branch AS RECEIBRANCH,<br />
                    NVL((select (Br.Gw_Bank_Code) || &#39;-&#39; || BR.TAD_NAME<br />
                    From TAD BR<br />
                    where Br.Gw_Bank_Code = vF21
                    <br />
                    --AND lpad(Br.Sibs_Bank_Code,5,&#39;0&#39;)=lpad(Receive_Branch,5,&#39;0&#39;)
                    <br />
                    ),<br />
                    &#39;&#39;) AS RECEIBRANCHNAME,<br />
                    <br />
                    Trans_Date AS TRANSDATE,<br />
                    Receiving_Time AS Receiving_Time,<br />
                    Sending_Time AS SEnding_Time,<br />
                    <br />
                    Pre_Tad AS PreTAD,<br />
                    (select Br.Sibs_Bank_Code || &#39;-&#39; || BR.Bran_Name<br />
                    From Branch BR<br />
                    where to_number(Br.Sibs_Bank_Code) = to_number(Pre_Tad)) AS PreTADName,<br />
                    <br />
                    Print_Sts AS PRINT_STATUS,<br />
                    Gw_Trans_Num AS BUTTOAN,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, &#39;011&#39;) as SOGD,<br />
                    <br />
                    Sender AS Sender,<br />
                    (select B.bank_name<br />
                    from IBPS_BANK_MAP B<br />
                    where B.gw_bank_code = Sender<br />
                    and rownum = 1) AS BankSenderName,<br />
                    <br />
                    Receiver AS Recerver,<br />
                    (select B.bank_name<br />
                    from IBPS_BANK_MAP B<br />
                    where B.gw_bank_code = Receiver<br />
                    and rownum = 1) AS BankReceiName,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, &#39;028&#39;) as sendname,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, &#39;029&#39;) as sendaddress,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, &#39;030&#39;) as sendacc,<br />
                    <br />
                    AtBankSend AS AtBankSend,<br />
                    (select B.bank_name<br />
                    from IBPS_BANK_MAP B<br />
                    where B.gw_bank_code = AtBankSend<br />
                    and rownum = 1) AS AtBankSendName,<br />
                    <br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, &#39;031&#39;) as receiname,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, &#39;032&#39;) as receiaddress,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, &#39;033&#39;) as receiacc,<br />
                    <br />
                    AtBankRecei AS AtBankRecei,<br />
                    (select B.bank_name<br />
                    from IBPS_BANK_MAP B<br />
                    where B.gw_bank_code = AtBankRecei<br />
                    and rownum = 1) AS AtBankReceiName,<br />
                    trim(STATUS) AS STATUS,<br />
                    Amount AS AMOUNT,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, &#39;026&#39;) as CCY,<br />
                    <br />
                    --GW_PK_IBPS_REPORT.IBPS_GET_Field(vContent, &#39;034&#39;) as NOIDUNG,<br />
                    Descriptions as NOIDUNG,<br />
                    Descriptions AS Descriptions,<br />
                    vSibs_TellerID AS TellerID,<br />
                    vUser AS TellerName,<br />
                    TransCode AS TRANS_CODE,<br />
                    (Select CV.V_READ from ccytovie CV where CV.CCYCODE LIKE vCCYCD and rownum=1) AS 
                    CCYNAME,<br />
                    &#39;2&#39; AS GRPID<br />
                    from dual;<br />
                    <br />
                    /* v_sql := &#39;select A.Msg_ID, A.Query_ID, A.Rm_Number, &#39;&#39;&#39; || vBranchName ||<br />
                    &#39;&#39;&#39; AS BRANCHNAME,A.MSG_DIRECTION,<br />
                    A.Source_Branch ,<br />
                    (select Br.Sibs_Bank_Code || &#39;&#39; - &#39;&#39; || BR.Bran_Name From Branch BR where 
                    to_number(Br.Sibs_Bank_Code)= A.Source_Branch) AS SRCBranchName,<br />
                    A.tad AS Receive_Branch,<br />
                    (select Br.Sibs_Bank_Code || &#39;&#39; - &#39;&#39; || BR.Bran_Name From Branch BR where 
                    to_number(Br.Sibs_Bank_Code)=to_number( A.tad)) AS RECBranchName,
                    <br />
                    A.Trans_Date,<br />
                    A.Receiving_Time,<br />
                    A.Sending_Time,
                    <br />
                    A.Pre_Tad,<br />
                    &#39;&#39;&#39;&#39; PreTADName,<br />
                    A.Print_Sts,<br />
                    A.Gw_Trans_Num AS SOBUTTOAN,<br />
                    A.Trans_Code,<br />
                    A.F07 AS Sender,<br />
                    (select B.bank_name<br />
                    from IBPS_BANK_MAP B<br />
                    where B.gw_bank_code = A.F07<br />
                    and rownum = 1) bank_sender,<br />
                    A.F19 AS Receiver,
                    <br />
                    (select B.bank_name<br />
                    from IBPS_BANK_MAP B<br />
                    where B.gw_bank_code = A.F19<br />
                    and rownum = 1) bank_receiver,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, &#39;&#39;028&#39;&#39;) as sendname,
                    <br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, &#39;&#39;029&#39;&#39;) as sendaddress,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, &#39;&#39;030&#39;&#39;) as sendacc,<br />
                    A.F21 AtBankSend,<br />
                    (select B.bank_name<br />
                    from IBPS_BANK_MAP B<br />
                    where B.gw_bank_code = A.F21<br />
                    and rownum = 1) AS AtBankSendName,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, &#39;&#39;031&#39;&#39;) as receiname,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, &#39;&#39;032&#39;&#39;) as receiaddress,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, &#39;&#39;033&#39;&#39;) as receiacc,
                    <br />
                    A.F22 AS AtBankRecei,<br />
                    (select B.bank_name<br />
                    from IBPS_BANK_MAP B<br />
                    where B.gw_bank_code = A.F22<br />
                    and rownum = 1) AtBankReceiName,<br />
                    (select S.name<br />
                    from status S<br />
                    where S.status = A.status<br />
                    and rownum = 1) STATUS,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, &#39;&#39;026&#39;&#39;) as CCY,<br />
                    A.Amount,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(A.content, &#39;&#39;034&#39;&#39;) as NOIDUNG,<br />
                    A.Trans_description as Descriptions,
                    <br />
                    A.Tellerid,<br />
                    &#39;&#39;noname&#39;&#39; AS TellerName
                    <br />
                    from &#39; || pTblTable || &#39; A<br />
                    where trim(A.MSG_ID) = trim(&#39; || pMSG_ID || &#39;)<br />
                    and rownum = 1&#39;;<br />
                    <br />
                    Open pcurBMIBPSMSG for v_sql;<br />
                    */<br />
                    END IBPS_PRINT_MSG_LIST;<br />
                    -------------------------<br />
                    <br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;INSERT_IBPS_OL2&quot; (pRM_NUMBER VARCHAR2,<br />
                    pBRANCHCREATE VARCHAR2,<br />
                    pREMARK VARCHAR2,<br />
                    pCONTENT VARCHAR2,<br />
                    pCCYCD VARCHAR2,<br />
                    pAMOUNT VARCHAR2,<br />
                    pRMBENA VARCHAR2,<br />
                    pTELLERID VARCHAR2,<br />
                    pRMPB40 VARCHAR2,<br />
                    pSENDERACC VARCHAR2,<br />
                    pATTRIBUTE1 VARCHAR2,<br />
                    pATTRIBUTE2 VARCHAR2,<br />
                    pATTRIBUTE3 VARCHAR2,<br />
                    pATTRIBUTE4 VARCHAR2,<br />
                    pATTRIBUTE5 VARCHAR2,<br />
                    pATTRIBUTE6 VARCHAR2,<br />
                    pATTRIBUTE7 VARCHAR2) is<br />
                    pragma autonomous_transaction;<br />
                    icount int;<br />
                    begin<br />
                    <br />
                    Insert into IBPS_SIBS_OL2<br />
                    (RM_NUMBER,<br />
                    BRANCHCREATE,<br />
                    REMARK,<br />
                    CONTENT,<br />
                    CCYCD,<br />
                    AMOUNT,<br />
                    RMBENA,<br />
                    TELLERID,<br />
                    RMPB40,<br />
                    SENDERACC,<br />
                    ATTRIBUTE1,<br />
                    ATTRIBUTE2,<br />
                    ATTRIBUTE3,<br />
                    ATTRIBUTE4,<br />
                    ATTRIBUTE5,<br />
                    ATTRIBUTE6,<br />
                    ATTRIBUTE7,<br />
                    transdate,<br />
                    status)<br />
                    values<br />
                    (pRM_NUMBER,<br />
                    pBRANCHCREATE,<br />
                    pREMARK,<br />
                    pCONTENT,<br />
                    pCCYCD,<br />
                    pAMOUNT,<br />
                    pRMBENA,<br />
                    pTELLERID,<br />
                    pRMPB40,<br />
                    pSENDERACC,<br />
                    pATTRIBUTE1,<br />
                    pATTRIBUTE2,<br />
                    pATTRIBUTE3,<br />
                    pATTRIBUTE4,<br />
                    pATTRIBUTE5,<br />
                    pATTRIBUTE6,<br />
                    pATTRIBUTE7,<br />
                    sysdate,<br />
                    0);<br />
                    <br />
                    IF (pATTRIBUTE5 = &#39;TAX&#39;) then<br />
                    select count(1)<br />
                    into icount<br />
                    from sysvar<br />
                    where Gwtype = &#39;IBPS&#39;<br />
                    and varname = &#39;IBPS_OL2_CHECKPOINT&#39;;<br />
                    if icount &gt; 0 then<br />
                    Update sysvar<br />
                    set value = pATTRIBUTE2<br />
                    where Gwtype = &#39;IBPS&#39;<br />
                    and varname = &#39;IBPS_OL2_CHECKPOINT&#39;;<br />
                    else<br />
                    insert into sysvar<br />
                    (ID, GWTYPE, VARNAME, VALUE, TYPE, NOTE, DESCRIPTION)<br />
                    values<br />
                    (3333,<br />
                    &#39;IBPS&#39;,<br />
                    &#39;IBPS_OL2_CHECKPOINT&#39;,<br />
                    pATTRIBUTE2,<br />
                    &#39;String&#39;,<br />
                    &#39;Check point khi lay dien Nop thue&#39;,<br />
                    &#39;Check point khi lay dien Nop thue&#39;);<br />
                    <br />
                    end if;<br />
                    select count(1)<br />
                    into icount<br />
                    from sysvar<br />
                    where Gwtype = &#39;IBPS&#39;<br />
                    and varname = &#39;IBPS_OL2_CHECKPOINT1&#39;;<br />
                    if icount &gt; 0 then<br />
                    Update sysvar<br />
                    set value = pRM_NUMBER<br />
                    where Gwtype = &#39;IBPS&#39;<br />
                    and varname = &#39;IBPS_OL2_CHECKPOINT1&#39;;<br />
                    else<br />
                    insert into sysvar<br />
                    (ID, GWTYPE, VARNAME, VALUE, TYPE, NOTE, DESCRIPTION)<br />
                    values<br />
                    (3333,<br />
                    &#39;IBPS&#39;,<br />
                    &#39;IBPS_OL2_CHECKPOINT1&#39;,<br />
                    pRM_NUMBER,<br />
                    &#39;String&#39;,<br />
                    &#39;Check point khi lay dien Nop thue&#39;,<br />
                    &#39;Check point khi lay dien Nop thue&#39;);<br />
                    <br />
                    end if;<br />
                    else<br />
                    select count(1)<br />
                    into icount<br />
                    from sysvar<br />
                    where Gwtype = &#39;IBPS&#39;<br />
                    and varname = &#39;IBPS_OL2_CHECKPOINT_IB&#39;;<br />
                    if icount &gt; 0 then<br />
                    Update sysvar<br />
                    set value = pATTRIBUTE2<br />
                    where Gwtype = &#39;IBPS&#39;<br />
                    and varname = &#39;IBPS_OL2_CHECKPOINT_IB&#39;;<br />
                    else<br />
                    insert into sysvar<br />
                    (ID, GWTYPE, VARNAME, VALUE, TYPE, NOTE, DESCRIPTION)<br />
                    values<br />
                    (111112,<br />
                    &#39;IBPS&#39;,<br />
                    &#39;IBPS_OL2_CHECKPOINT_IB&#39;,<br />
                    pATTRIBUTE2,<br />
                    &#39;String&#39;,<br />
                    &#39;Check point khi lay dien Nop thue&#39;,<br />
                    &#39;Check point khi lay dien Nop thue&#39;);<br />
                    <br />
                    end if;<br />
                    select count(1)<br />
                    into icount<br />
                    from sysvar<br />
                    where Gwtype = &#39;IBPS&#39;<br />
                    and varname = &#39;IBPS_OL2_CHECKPOINT_IB1&#39;;<br />
                    if icount &gt; 0 then<br />
                    Update sysvar<br />
                    set value = pRM_NUMBER<br />
                    where Gwtype = &#39;IBPS&#39;<br />
                    and varname = &#39;IBPS_OL2_CHECKPOINT_IB1&#39;;<br />
                    else<br />
                    insert into sysvar<br />
                    (ID, GWTYPE, VARNAME, VALUE, TYPE, NOTE, DESCRIPTION)<br />
                    values<br />
                    (21231,<br />
                    &#39;IBPS&#39;,<br />
                    &#39;IBPS_OL2_CHECKPOINT_IB1&#39;,<br />
                    pRM_NUMBER,<br />
                    &#39;String&#39;,<br />
                    &#39;Check point khi lay dien Nop thue&#39;,<br />
                    &#39;Check point khi lay dien Nop thue&#39;);<br />
                    <br />
                    end if;<br />
                    end if;<br />
                    commit;<br />
                    end INSERT_IBPS_OL2;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;INSERTIBPS_MSG_REC_TEMP&quot; (vRM_Number Varchar2,<br />
                    nAmount number,<br />
                    vCCY varchar2,<br />
                    vSender varchar2,<br />
                    vReceiver varchar2,<br />
                    vtransdate varchar2,<br />
                    Department varchar2,<br />
                    vMsgDirection varchar2) IS<br />
                    pragma autonomous_transaction;<br />
                    m_IBPS_MSG_REC_TEMP IBPS_MSG_REC_TEMP%Rowtype;<br />
                    icount int;<br />
                    Begin<br />
                    m_IBPS_MSG_REC_TEMP.Gw_Type := &#39;IBPS&#39;;<br />
                    m_IBPS_MSG_REC_TEMP.Rm_Number := vRM_Number;<br />
                    m_IBPS_MSG_REC_TEMP.Msg_Direction := vMsgDirection;<br />
                    if (vMsgDirection=&#39;O&#39;) then<br />
                    m_IBPS_MSG_REC_TEMP.From_System := &#39;SIBS&#39;;<br />
                    m_IBPS_MSG_REC_TEMP.To_System := &#39;FPT&#39;;<br />
                    else<br />
                    m_IBPS_MSG_REC_TEMP.From_System := &#39;FPT&#39;;<br />
                    m_IBPS_MSG_REC_TEMP.To_System := &#39;SIBS&#39;;<br />
                    end if;
                    <br />
                    <br />
                    m_IBPS_MSG_REC_TEMP.Trans_Date := to_date(vtransdate, &#39;YYYYMMDD&#39;);<br />
                    m_IBPS_MSG_REC_TEMP.Ndate := to_number(vtransdate);<br />
                    m_IBPS_MSG_REC_TEMP.Rec_Type := &#39;SIBS-BR&#39;;<br />
                    -- Build m_IBPS_MSG_REC_TEMP<br />
                    m_IBPS_MSG_REC_TEMP.Gw_Type := &#39;IBPS&#39;;<br />
                    m_IBPS_MSG_REC_TEMP.RM_NUMBER := vRM_Number;<br />
                    m_IBPS_MSG_REC_TEMP.Ref_number := vRM_Number;<br />
                    m_IBPS_MSG_REC_TEMP.Receiver := vReceiver;<br />
                    m_IBPS_MSG_REC_TEMP.Ccy := vCCY;<br />
                    m_IBPS_MSG_REC_TEMP.App_Code := Department;<br />
                    m_IBPS_MSG_REC_TEMP.Sender := vSender;<br />
                    m_IBPS_MSG_REC_TEMP.Amount := To_number(nAmount)*100;<br />
                    <br />
                    m_IBPS_MSG_REC_TEMP.Msg_Direction := vMsgDirection;<br />
                    <br />
                    m_IBPS_MSG_REC_TEMP.Trans_Date := to_date(vtransdate, &#39;YYYYMMDD&#39;);<br />
                    <br />
                    m_IBPS_MSG_REC_TEMP.Ndate := to_number(vtransdate);<br />
                    m_IBPS_MSG_REC_TEMP.Rec_Type := &#39;SIBS-BR&#39;;<br />
                    --if m_IBPS_MSG_REC_TEMP.Sender = &#39;11&#39; then<br />
                    <br />
                    select count(1)<br />
                    into icount<br />
                    from IBPS_MSG_REC_TEMP vc<br />
                    where vc.rm_number = m_IBPS_MSG_REC_TEMP.Rm_Number<br />
                    and m_IBPS_MSG_REC_TEMP.Rec_Type = &#39;SIBS-BR&#39;<br />
                    and vc.ndate = m_IBPS_MSG_REC_TEMP.Ndate;<br />
                    <br />
                    if icount = 0 then<br />
                    /*select count(1)<br />
                    into icount<br />
                    from TAD vc<br />
                    where LTRIM(vc.sibs_code, &#39;0&#39;) =<br />
                    LTRIM(m_IBPS_MSG_REC_TEMP.Sender, &#39;0&#39;);*/<br />
                    --if icount &gt; 0 then<br />
                    Begin<br />
                    select vc.gw_bank_code<br />
                    into m_IBPS_MSG_REC_TEMP.Sender<br />
                    from TAD vc<br />
                    where LTRIM(vc.sibs_code, &#39;0&#39;) =<br />
                    LTRIM(m_IBPS_MSG_REC_TEMP.Sender, &#39;0&#39;);<br />
                    Exception<br />
                    when others then<br />
                    m_IBPS_MSG_REC_TEMP.Sender := &#39;01302001&#39;;<br />
                    end;<br />
                    insert into IBPS_MSG_REC_TEMP values m_IBPS_MSG_REC_TEMP;<br />
                    commit;<br />
                    --end if;<br />
                    end if;<br />
                    commit;<br />
                    <br />
                    exception<br />
                    when others then<br />
                    GW_PK_RECONCILE.Reconcile_trace(&#39;[SIBS-BR][IBPS]&#39;,<br />
                    &#39;=&gt; miss one message&#39; || sqlerrm);<br />
                    End InsertIBPS_MSG_REC_TEMP;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;INSERTSWIFT_RECTEMP&quot; (vRM_Number Varchar2,<br />
                    vRef_number varchar2,<br />
                    vMSG_type varchar2,<br />
                    nAmount varchar2,<br />
                    vCCY varchar2,<br />
                    vSender varchar2,<br />
                    vReceiver varchar2,<br />
                    vtransdate varchar2,<br />
                    vValuedate varchar2,<br />
                    Department varchar2,<br />
                    vMsgDirection varchar2) is<br />
                    pragma autonomous_transaction;<br />
                    iNum int;<br />
                    m_SWIFT_MSG_REC_TEMP SWIFT_MSG_REC_TEMP%Rowtype;<br />
                    bb varchar2(4000);<br />
                    Begin<br />
                    <br />
                    m_SWIFT_MSG_REC_TEMP.Gw_Type := &#39;SWIFT&#39;;<br />
                    m_SWIFT_MSG_REC_TEMP.RM_NUMBER := vRM_Number;<br />
                    m_SWIFT_MSG_REC_TEMP.Acc_Type := &#39;&#39;;<br />
                    <br />
                    m_SWIFT_MSG_REC_TEMP.REF_number := vRef_number;<br />
                    <br />
                    m_SWIFT_MSG_REC_TEMP.Msg_Type := vMSG_type;<br />
                    <br />
                    m_SWIFT_MSG_REC_TEMP.Ccy := vCCY;<br />
                    if nAmount = null then<br />
                    m_SWIFT_MSG_REC_TEMP.Amount := 0;<br />
                    else<br />
                    m_SWIFT_MSG_REC_TEMP.Amount := to_number(nAmount)*100;<br />
                    end if;<br />
                    <br />
                    <br />
                    m_SWIFT_MSG_REC_TEMP.App_Code := Department;<br />
                    <br />
                    m_SWIFT_MSG_REC_TEMP.Sender := vSender;<br />
                    m_SWIFT_MSG_REC_TEMP.Receiver := vReceiver;<br />
                    <br />
                    m_SWIFT_MSG_REC_TEMP.Msg_Direction := vMsgDirection;<br />
                    if (vMsgDirection=&#39;O&#39;) then<br />
                    m_SWIFT_MSG_REC_TEMP.From_System := &#39;SIBS&#39;;<br />
                    m_SWIFT_MSG_REC_TEMP.To_System := &#39;FPT&#39;;<br />
                    else<br />
                    m_SWIFT_MSG_REC_TEMP.From_System := &#39;FPT&#39;;<br />
                    m_SWIFT_MSG_REC_TEMP.To_System := &#39;SIBS&#39;;<br />
                    end if;
                    <br />
                    m_SWIFT_MSG_REC_TEMP.Trans_Date := to_date(vtransdate, &#39;YYYYMMDD&#39;);<br />
                    <br />
                    m_SWIFT_MSG_REC_TEMP.Ndate := to_number(vtransdate);<br />
                    <br />
                    <br />
                    m_SWIFT_MSG_REC_TEMP.Rec_Type := &#39;SIBS-BR&#39;;<br />
                    -- Check Duplicate: Factor{Transdate,Rec_type,RM_NUMBER,Ref_number,MsgType}<br />
                    select count(1)<br />
                    into iNum<br />
                    from SWIFT_MSG_REC_TEMP<br />
                    where trans_date || Rec_Type || RM_NUMBER || REF_number || Msg_Type ||<br />
                    App_code =<br />
                    m_SWIFT_MSG_REC_TEMP.trans_date || &#39;SIBS-BR&#39; ||<br />
                    m_SWIFT_MSG_REC_TEMP.RM_NUMBER || m_SWIFT_MSG_REC_TEMP.REF_number ||<br />
                    m_SWIFT_MSG_REC_TEMP.Msg_Type || Department;<br />
                    -- Tam thoi khong lay chieu dien di<br />
                    <br />
                    insert into SWIFT_MSG_REC_TEMP values m_SWIFT_MSG_REC_TEMP;<br />
                    commit;<br />
                    <br />
                    --Reconcile_trace(&#39;[SIBS-GW][SWIFT]&#39;,&#39;=&gt; Success&#39;);<br />
                    <br />
                    Exception<br />
                    When others then<br />
                    GW_PK_RECONCILE.Reconcile_trace(&#39;[SIBS-BR][SWIFT]&#39;,<br />
                    &#39;=&gt; miss one message&#39; || sqlerrm);<br />
                    End InsertSwift_Rectemp;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;INSERTVCB_RECTEMP&quot; (vRM_Number Varchar2,<br />
                    vRef_number varchar2,<br />
                    nAmount number,<br />
                    vCCY varchar2,<br />
                    vSender varchar2,<br />
                    vReceiver varchar2,<br />
                    vtransdate varchar2,<br />
                    Department varchar2,<br />
                    vMsgDirection varchar2) is<br />
                    pragma autonomous_transaction;<br />
                    icount int;<br />
                    m_VCB_MSG_REC_TEMP VCB_MSG_REC_TEMP%Rowtype;<br />
                    Begin<br />
                    m_VCB_MSG_REC_TEMP.Gw_Type := &#39;VCB&#39;;<br />
                    m_VCB_MSG_REC_TEMP.RM_NUMBER := vRef_number;<br />
                    m_VCB_MSG_REC_TEMP.Ref_number := vRef_number;<br />
                    m_VCB_MSG_REC_TEMP.Trans_no := vRef_number;<br />
                    m_VCB_MSG_REC_TEMP.Msg_Type := &#39;MT103&#39;;<br />
                    m_VCB_MSG_REC_TEMP.Ccy := vCCY;<br />
                    m_VCB_MSG_REC_TEMP.App_Code := Department;<br />
                    m_VCB_MSG_REC_TEMP.Sender := vSender;<br />
                    <br />
                    m_VCB_MSG_REC_TEMP.Amount := nAmount*100;<br />
                    <br />
                    m_VCB_MSG_REC_TEMP.Trans_Date := to_date(vtransdate, &#39;YYYYMMDD&#39;);<br />
                    <br />
                    m_VCB_MSG_REC_TEMP.Value_Date := sysdate;<br />
                    <br />
                    m_VCB_MSG_REC_TEMP.Msg_Direction := vMsgDirection;<br />
                    if (vMsgDirection = &#39;O&#39;) then<br />
                    m_VCB_MSG_REC_TEMP.From_System := &#39;SIBS&#39;;<br />
                    m_VCB_MSG_REC_TEMP.To_System := &#39;FPT&#39;;<br />
                    else<br />
                    m_VCB_MSG_REC_TEMP.From_System := &#39;FPT&#39;;<br />
                    m_VCB_MSG_REC_TEMP.To_System := &#39;SIBS&#39;;<br />
                    end if;<br />
                    --m_VCB_MSG_REC_TEMP.Trans_Date := sysdate;<br />
                    <br />
                    m_VCB_MSG_REC_TEMP.Ndate := to_number(vtransdate);<br />
                    m_VCB_MSG_REC_TEMP.Rec_Type := &#39;SIBS-BR&#39;;<br />
                    --if trim(m_VCB_MSG_REC_TEMP.Sender) = &#39;11&#39; then<br />
                    select count(1)<br />
                    into icount<br />
                    from VCB_MSG_REC_TEMP vc<br />
                    where vc.rm_number = m_VCB_MSG_REC_TEMP.Rm_Number<br />
                    and m_VCB_MSG_REC_TEMP.Rec_Type = &#39;SIBS-BR&#39;<br />
                    AND VC.NDATE = m_VCB_MSG_REC_TEMP.Ndate;<br />
                    if icount = 0 then<br />
                    insert into VCB_MSG_REC_TEMP values m_VCB_MSG_REC_TEMP;<br />
                    commit;<br />
                    end if;<br />
                    Exception<br />
                    when others then<br />
                    GW_PK_RECONCILE.Reconcile_trace(&#39;[SIBS-BR][VCB]&#39;,<br />
                    &#39;=&gt; miss one message&#39; || sqlerrm);<br />
                    End InsertVCB_Rectemp;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;MOI&quot; (pfrdate in date,<br />
                    ptodate in date,<br />
                    RefCurMOI IN OUT PKG_CR.RefCurType) IS<br />
                    NumOfDate NUMBER;<br />
                    LVMsgIN NUMBER(38);<br />
                    LVMsgOUT NUMBER(38);<br />
                    BEGIN<br />
                    <br />
                    --insert into test_clob values (TO_DATE(frdate,&#39;DD/MM/YYYY&#39;) || &#39;-&#39; || 
                    TO_DATE(todate,&#39;DD/MM/YYYY&#39;));<br />
                    commit;<br />
                    <br />
                    ---Tong gia tri dien di gia tri thap<br />
                    /* Formatted on 2008/08/10 20:58 (Formatter Plus v4.8.6) */<br />
                    SELECT SUM(AMOUNT)<br />
                    into LVMsgOUT<br />
                    FROM (SELECT amount, msg_id<br />
                    FROM ibps_msg_content<br />
                    WHERE msg_direction = &#39;SIBS-IBPS&#39;<br />
                    AND trans_code = &#39;101001&#39;<br />
                    AND status = 1<br />
                    and transdate &gt;= to_char(pfrdate, &#39;YYYYMMDD&#39;)<br />
                    AND transdate &lt;= to_char(ptodate, &#39;YYYYMMDD&#39;)<br />
                    UNION ALL<br />
                    SELECT amount, msg_id<br />
                    FROM ibps_msg_ALL<br />
                    WHERE msg_direction = &#39;SIBS-IBPS&#39;<br />
                    AND trans_code = &#39;101001&#39;<br />
                    AND status = 1<br />
                    and transdate &gt;= to_char(pfrdate, &#39;YYYYMMDD&#39;)<br />
                    AND transdate &lt;= to_char(ptodate, &#39;YYYYMMDD&#39;)<br />
                    UNION ALL<br />
                    SELECT amount, msg_id<br />
                    FROM ibps_msg_ALL_HIS<br />
                    WHERE msg_direction = &#39;SIBS-IBPS&#39;<br />
                    AND trans_code = &#39;101001&#39;<br />
                    AND status = 1<br />
                    and transdate &gt;= to_char(pfrdate, &#39;YYYYMMDD&#39;)<br />
                    AND transdate &lt;= to_char(ptodate, &#39;YYYYMMDD&#39;));<br />
                    <br />
                    ---Tong gia tri dien den gia tri thap<br />
                    SELECT SUM(amount)<br />
                    into LVMsgIN<br />
                    FROM (SELECT amount, msg_id<br />
                    FROM ibps_msg_content<br />
                    WHERE msg_direction = &#39;IBPS-SIBS&#39;<br />
                    AND trans_code = &#39;101001&#39;<br />
                    AND status = 1<br />
                    and transdate &gt;= to_char(pfrdate, &#39;YYYYMMDD&#39;)<br />
                    AND transdate &lt;= to_char(ptodate, &#39;YYYYMMDD&#39;)<br />
                    UNION ALL<br />
                    SELECT amount, msg_id<br />
                    FROM ibps_msg_ALL<br />
                    WHERE msg_direction = &#39;IBPS-SIBS&#39;<br />
                    AND trans_code = &#39;101001&#39;<br />
                    AND status = 1<br />
                    and transdate &gt;= to_char(pfrdate, &#39;YYYYMMDD&#39;)<br />
                    AND transdate &lt;= to_char(ptodate, &#39;YYYYMMDD&#39;)<br />
                    UNION ALL<br />
                    SELECT amount, msg_id<br />
                    FROM ibps_msg_ALL_HIS<br />
                    WHERE msg_direction = &#39;IBPS-SIBS&#39;<br />
                    AND trans_code = &#39;101001&#39;<br />
                    AND status = 1<br />
                    and transdate &gt;= to_char(pfrdate, &#39;YYYYMMDD&#39;)<br />
                    AND transdate &lt;= to_char(ptodate, &#39;YYYYMMDD&#39;));<br />
                    <br />
                    NumOfDate := ptodate - pfrdate + 1;<br />
                    <br />
                    open RefCurMOI for<br />
                    select to_char(nvl(LVMsgOUT, 0)) OV,<br />
                    to_char(nvl(LVMsgIN, 0)) IV,<br />
                    NumOfDate DAYS,<br />
                    to_char((LVMsgOUT - nvl(LVMsgIN, 0)) / NumOfDate) as Net<br />
                    from dual;<br />
                    -- select * from IBPS_MSG_CONTENT;<br />
                    END MOI;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;MSB_BM_IBPS_02&quot; (pDate in date,<br />
                    pCitad in varchar2, --tad 8 so<br />
                    pCcycd in varchar2 default &#39;VND&#39;,<br />
                    pStatus in varchar2,<br />
                    RefCurBM_IBPS02 IN OUT PKG_CR.RefCurType) IS<br />
                    --vDate date;<br />
                    vCitad varchar2(50);<br />
                    vCcycd varchar2(3);<br />
                    vStatus varchar2(50);<br />
                    vOndate date;<br />
                    BEGIN<br />
                    <br />
                    select sysdate into vOndate from dual;<br />
                    if upper(trim(pCitad)) = &#39;ALL&#39; or trim(pCitad) is null then<br />
                    vCitad := &#39;%&#39;;<br />
                    else<br />
                    vCitad := trim(pCitad);<br />
                    end if;<br />
                    if upper(trim(pCcycd)) = &#39;ALL&#39; or trim(pCcycd) is null then<br />
                    vCcycd := &#39;%&#39;;<br />
                    else<br />
                    vCcycd := trim(pCcycd);<br />
                    end if;<br />
                    if upper(trim(pStatus)) = &#39;ALL&#39; or trim(pStatus) is null then<br />
                    vStatus := &#39;%&#39;;<br />
                    else<br />
                    vStatus := trim(pStatus);<br />
                    end if;<br />
                    <br />
                    if to_char(pDate, &#39;DDMMYYYY&#39;)= to_char(vOndate, &#39;DDMMYYYY&#39;) then<br />
                    --open RefCurBM_IBPS02 for<br />
                    delete from BM_IBPS02_TEMP;<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    TRAN_CODE,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,
                    <br />
                    decode(trim(pCitad),&#39;ALL&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),
                    <br />
                    A.Tellerid,<br />
                    BR.Bran_Name, -- tad name<br />
                    BR1.Bran_Name -- pre tad name<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS BUTTOAN,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;011&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.tellerid,<br />
                    C.F21,<br />
                    C.F19<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND nvl(C.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 like vCitad ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    <br />
                    LEFT JOIN BRANCH BR ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN BRANCH BR1 ON BR1.SIBS_BANK_CODE =<br />
                    SUBSTR(A.Pre_Tad, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    <br />
                    WHERE B.CONTENT LIKE vStatus;
                    <br />
                    elsif substr(to_char(pDate, &#39;DDMMYYYY&#39;), 3, 6) =<br />
                    substr(to_char(vOndate, &#39;DDMMYYYY&#39;),<br />
                    3,<br />
                    6) then<br />
                    delete from BM_IBPS02_TEMP;<br />
                    --Lay du lieu trong ngay<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    decode(trim(pCitad),&#39;ALL&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),
                    <br />
                    A.Tellerid,<br />
                    BR.Bran_Name, -- tad name<br />
                    BR1.Bran_Name -- pre tad name<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.TellerID,<br />
                    C.F21,<br />
                    C.F19<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND nvl(C.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 like vCitad) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    <br />
                    LEFT JOIN BRANCH BR ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN BRANCH BR1 ON BR1.SIBS_BANK_CODE =<br />
                    SUBSTR(A.Pre_Tad, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    <br />
                    WHERE B.CONTENT LIKE vStatus;<br />
                    --Lay du lieu trong thang<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    decode(trim(pCitad),&#39;ALL&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),
                    <br />
                    A.Tellerid,<br />
                    BR.Bran_Name, -- tad name<br />
                    BR1.Bran_Name -- pre tad name<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid,<br />
                    C.F21,<br />
                    C.F19<br />
                    FROM IBPS_MSG_ALL C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND nvl(C.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 like vCitad) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    <br />
                    LEFT JOIN BRANCH BR ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN BRANCH BR1 ON BR1.SIBS_BANK_CODE =<br />
                    SUBSTR(A.Pre_Tad, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    <br />
                    WHERE B.CONTENT LIKE vStatus;<br />
                    <br />
                    else<br />
                    delete from BM_IBPS02_TEMP;<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    decode(trim(pCitad),&#39;ALL&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),
                    <br />
                    A.Tellerid,<br />
                    BR.Bran_Name, -- tad name<br />
                    BR1.Bran_Name -- pre tad name<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid,<br />
                    C.F21,<br />
                    C.F19<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND nvl(C.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 like vCitad) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    <br />
                    LEFT JOIN BRANCH BR ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN BRANCH BR1 ON BR1.SIBS_BANK_CODE =<br />
                    SUBSTR(A.Pre_Tad, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    <br />
                    WHERE B.CONTENT LIKE vStatus;<br />
                    --Lay du lieu trong thang<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    decode(trim(pCitad),&#39;ALL&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),
                    <br />
                    A.Tellerid,<br />
                    BR.Bran_Name, -- tad name<br />
                    BR1.Bran_Name -- pre tad name<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid,<br />
                    C.F21,<br />
                    C.F19<br />
                    FROM IBPS_MSG_ALL C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND nvl(C.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 like vCitad) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    <br />
                    LEFT JOIN BRANCH BR ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN BRANCH BR1 ON BR1.SIBS_BANK_CODE =<br />
                    SUBSTR(A.Pre_Tad, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    <br />
                    WHERE B.CONTENT LIKE vStatus;<br />
                    --Lay du lieu trong nam<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    decode(trim(pCitad),&#39;ALL&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),
                    <br />
                    A.Tellerid,<br />
                    BR.Bran_Name, -- tad name<br />
                    BR1.Bran_Name -- pre tad name<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid,<br />
                    C.F21,<br />
                    C.F19<br />
                    FROM IBPS_MSG_ALL_HIS C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND nvl(C.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 like vCitad) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    <br />
                    LEFT JOIN BRANCH BR ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN BRANCH BR1 ON BR1.SIBS_BANK_CODE =<br />
                    SUBSTR(A.Pre_Tad, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    <br />
                    WHERE B.CONTENT LIKE vStatus;<br />
                    end if;<br />
                    <br />
                    -- get from backup
                    <br />
                    delete from BM_IBPS02_TEMP where query_id in (<br />
                    select query_id from ibps_msg_content_backup C
                    <br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(C.STATUS) =<br />
                    trim(B.STATUS)<br />
                    where<br />
                    to_char(TRANS_DATE,&#39;YYYYMMDD&#39;)=to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    and to_char(create_Date,&#39;YYYYMMDD&#39;)&lt;&gt;to_char(C.TRANS_DATE, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND nvl(C.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 like vCitad<br />
                    AND B.CONTENT LIKE vStatus<br />
                    );<br />
                    <br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    ,EXTFIELD04<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    decode(trim(pCitad),&#39;ALL&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),
                    <br />
                    A.Tellerid,<br />
                    BR.Bran_Name, -- tad name<br />
                    BR1.Bran_Name -- pre tad name<br />
                    ,1<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid,<br />
                    C.F21,<br />
                    C.F19<br />
                    FROM IBPS_MSG_Content_backup C<br />
                    WHERE
                    <br />
                    C.Id IN
                    <br />
                    (Select Max(C1.ID) from IBPS_MSG_Content_backup C1
                    <br />
                    where C1.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    and to_char(C1.create_Date,&#39;YYYYMMDD&#39;)&lt;&gt;to_char(C1.TRANS_DATE, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C1.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND nvl(C1.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    AND C1.F21 like vCitad<br />
                    group by C1.Query_Id)<br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    <br />
                    LEFT JOIN BRANCH BR ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN BRANCH BR1 ON BR1.SIBS_BANK_CODE =<br />
                    SUBSTR(A.Pre_Tad, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    <br />
                    WHERE B.CONTENT LIKE vStatus;<br />
                    <br />
                    open RefCurBM_IBPS02 for<br />
                    select * from BM_IBPS02_TEMP order by GW_TRANS_NUM, RM_NUMBER asc;<br />
                    <br />
                    end MSB_BM_IBPS_02;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;MSB_BM_IBPS_02_CN&quot; (pDate in date,<br />
                    pCitad in varchar2, --sender<br />
                    pCcycd in varchar2 default &#39;VND&#39;,<br />
                    pStatus in varchar2,<br />
                    RefCurBM_IBPS02_CN IN OUT PKG_CR.RefCurType) IS<br />
                    --vDate date;<br />
                    vSender varchar2(50); --sender 8 so<br />
                    vCcycd varchar2(3);<br />
                    vStatus varchar2(50);<br />
                    vSrcBranch varchar2(50);<br />
                    vOndate date;<br />
                    vBranchName varchar2(200);<br />
                    BEGIN<br />
                    <br />
                    select sysdate into vOndate from dual;<br />
                    if upper(trim(pCitad)) = &#39;ALL&#39; or trim(pCitad) is null then<br />
                    vSender := &#39;%&#39;;<br />
                    vSrcBranch := &#39;%&#39;;<br />
                    vBranchname := &#39;ALL&#39;;<br />
                    else<br />
                    GW_PK_IBPS_REPORT.SPLIT_SENDER(trim(pCitad), &#39;-&#39;, vSrcBranch, vSender);<br />
                    vSrcBranch := lpad(vSrcBranch, 5, &#39;0&#39;);<br />
                    if vSrcBranch = &#39;00011&#39; then<br />
                    select substr(T.Sibs_Bank_Code, -3) || &#39;-&#39; || T.Tad_Name<br />
                    into vBranchName<br />
                    from tad T<br />
                    where T.Gw_Bank_Code = vSender;<br />
                    else<br />
                    select substr(M.Sibs_Bank_Code, -3) || &#39;-&#39; || M.Bank_Name<br />
                    into vBranchName<br />
                    from ibps_bank_map M<br />
                    where M.Sibs_Bank_Code &lt;&gt; -1<br />
                    and lpad(M.Sibs_Bank_Code, 5, &#39;0&#39;) = (vSrcBranch)<br />
                    and M.Gw_Bank_Code = vSender;<br />
                    end if;<br />
                    end if;<br />
                    if upper(trim(pCcycd)) = &#39;ALL&#39; or trim(pCcycd) is null then<br />
                    vCcycd := &#39;%&#39;;<br />
                    else<br />
                    vCcycd := trim(pCcycd);<br />
                    end if;<br />
                    if upper(trim(pStatus)) = &#39;ALL&#39; or trim(pStatus) is null then<br />
                    vStatus := &#39;%&#39;;<br />
                    else<br />
                    vStatus := trim(pStatus);<br />
                    end if;<br />
                    <br />
                    if to_char(pDate, &#39;DDMMYYYY&#39;) = to_char(vOndate, &#39;DDMMYYYY&#39;) then<br />
                    --open RefCurBM_IBPS02 for<br />
                    delete from BM_IBPS02_TEMP;<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    TRAN_CODE,<br />
                    Sendername,<br />
                    Receivername,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    upper(trim(A.CCYCD)),<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    --decode(pCitad,&#39;ALL&#39;,&#39;ALL&#39;, substr(BSEND.SIBS_BANK_CODE,-3) || &#39; - &#39; || 
                    BSEND.BANK_NAME) AS BRAN_NAME,
                    <br />
                    vBranchName,<br />
                    A.Tellerid,<br />
                    BR.Gw_Bank_Code || &#39; - &#39; || BR.TAD_NAME AS TADNAME, -- tad name<br />
                    BR1.SIBS_BANK_CODE || &#39; - &#39; || BR1.BRAN_NAME AS PRETADNAME -- pre tad name<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.Tellerid,<br />
                    C.F21<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND nvl(C.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    /*AND C.F07 LIKE vSender
                    <br />
                    AND lpad(C.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch*/<br />
                    <br />
                    AND ((nvl(C.Product_Type, &#39;%&#39;) = &#39;OL3&#39; AND<br />
                    (lpad(C.Tad, 5, &#39;0&#39;) like vSrcBranch OR<br />
                    (C.F07 LIKE vSender AND<br />
                    lpad(C.Source_Branch, 5, &#39;0&#39;) LIKE vSrcBranch))) OR<br />
                    (nvl(C.Product_Type, &#39;%&#39;) &lt;&gt; &#39;OL3&#39; AND<br />
                    C.F07 LIKE vSender AND<br />
                    lpad(C.Source_Branch, 5, &#39;0&#39;) LIKE vSrcBranch))) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(A.STATUS) = trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND<br />
                    on BSEND.gw_bank_code = A.SENDER<br />
                    AND BSEND.Sibs_Bank_Code =<br />
                    decode(BSEND.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV<br />
                    on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND BRECV.Sibs_Bank_Code =<br />
                    decode(BRECV.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    --LEFT JOIN IBPS_BANK_MAP BR ON BR.Gw_Bank_Code = (A.F21)
                    <br />
                    -- AND BR.Sibs_Bank_Code=decode(BR.Sibs_Bank_Code,-1,-1,lpad(A.TAD,5,&#39;0&#39;))
                    <br />
                    LEFT JOIN TAD BR<br />
                    ON BR.Gw_Bank_Code = A.F21<br />
                    LEFT JOIN BRANCH BR1<br />
                    ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)<br />
                    <br />
                    WHERE B.CONTENT LIKE vStatus;<br />
                    elsif substr(to_char(pDate, &#39;DDMMYYYY&#39;), 3, 6) =<br />
                    substr(to_char(vOndate, &#39;DDMMYYYY&#39;), 3, 6) then<br />
                    delete from BM_IBPS02_TEMP;<br />
                    --Lay du lieu trong ngay<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    upper(trim(A.CCYCD)),<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    --decode(pCitad,&#39;ALL&#39;,&#39;ALL&#39;, substr(BSEND.SIBS_BANK_CODE,-3) || &#39; - &#39; || 
                    BSEND.BANK_NAME) AS BRAN_NAME,
                    <br />
                    vBranchName,<br />
                    A.Tellerid,<br />
                    BR.GW_BANK_CODE || &#39; - &#39; || BR.Tad_Name AS TADNAME, -- tad name<br />
                    BR1.SIBS_BANK_CODE || &#39; - &#39; || BR1.BRAN_NAME AS PRETADNAME -- pre tad name<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.Tellerid,<br />
                    C.F21<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND nvl(C.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    /*AND C.F07 LIKE vSender
                    <br />
                    AND lpad(C.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch*/<br />
                    <br />
                    AND ((nvl(C.Product_Type, &#39;%&#39;) = &#39;OL3&#39; AND<br />
                    (lpad(C.Tad, 5, &#39;0&#39;) like vSrcBranch OR<br />
                    (C.F07 LIKE vSender AND<br />
                    lpad(C.Source_Branch, 5, &#39;0&#39;) LIKE vSrcBranch))) OR<br />
                    (nvl(C.Product_Type, &#39;%&#39;) &lt;&gt; &#39;OL3&#39; AND<br />
                    C.F07 LIKE vSender AND<br />
                    lpad(C.Source_Branch, 5, &#39;0&#39;) LIKE vSrcBranch))) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(A.STATUS) = trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND<br />
                    on BSEND.gw_bank_code = A.SENDER<br />
                    AND BSEND.Sibs_Bank_Code =<br />
                    decode(BSEND.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV<br />
                    on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND BRECV.Sibs_Bank_Code =<br />
                    decode(BRECV.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    --LEFT JOIN IBPS_BANK_MAP BR ON BR.Gw_Bank_Code = (A.F21)
                    <br />
                    -- AND BR.Sibs_Bank_Code=decode(BR.Sibs_Bank_Code,-1,-1,lpad(A.TAD,5,&#39;0&#39;))
                    <br />
                    LEFT JOIN TAD BR<br />
                    ON BR.Gw_Bank_Code = A.F21<br />
                    LEFT JOIN BRANCH BR1<br />
                    ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)<br />
                    WHERE B.CONTENT LIKE vStatus<br />
                    order by A.taD, A.Trans_Code, RECEIVER asc;<br />
                    --Lay du lieu trong thang<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    upper(trim(A.CCYCD)),<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    --decode(pCitad,&#39;ALL&#39;,&#39;ALL&#39;, substr(BSEND.SIBS_BANK_CODE,-3) || &#39; - &#39; || 
                    BSEND.BANK_NAME) AS BRAN_NAME,
                    <br />
                    vBranchName,<br />
                    A.Tellerid,<br />
                    BR.GW_BANK_CODE || &#39; - &#39; || BR.Tad_Name AS TADNAME, -- tad name<br />
                    BR1.SIBS_BANK_CODE || &#39; - &#39; || BR1.BRAN_NAME AS PRETADNAME -- pre tad name<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.Tellerid,<br />
                    C.F21<br />
                    FROM IBPS_MSG_ALL C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND nvl(C.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    /*AND C.F07 LIKE vSender
                    <br />
                    AND lpad(C.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch*/<br />
                    <br />
                    AND ((nvl(C.Product_Type, &#39;%&#39;) = &#39;OL3&#39; AND<br />
                    (lpad(C.Tad, 5, &#39;0&#39;) like vSrcBranch OR<br />
                    (C.F07 LIKE vSender AND<br />
                    lpad(C.Source_Branch, 5, &#39;0&#39;) LIKE vSrcBranch))) OR<br />
                    (nvl(C.Product_Type, &#39;%&#39;) &lt;&gt; &#39;OL3&#39; AND<br />
                    C.F07 LIKE vSender AND<br />
                    lpad(C.Source_Branch, 5, &#39;0&#39;) LIKE vSrcBranch))) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(A.STATUS) = trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND<br />
                    on BSEND.gw_bank_code = A.SENDER<br />
                    AND BSEND.Sibs_Bank_Code =<br />
                    decode(BSEND.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV<br />
                    on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND BRECV.Sibs_Bank_Code =<br />
                    decode(BRECV.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    --LEFT JOIN IBPS_BANK_MAP BR ON BR.Gw_Bank_Code = (A.F21)
                    <br />
                    -- AND BR.Sibs_Bank_Code=decode(BR.Sibs_Bank_Code,-1,-1,lpad(A.TAD,5,&#39;0&#39;))
                    <br />
                    LEFT JOIN TAD BR<br />
                    ON BR.Gw_Bank_Code = A.F21<br />
                    LEFT JOIN BRANCH BR1<br />
                    ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)<br />
                    WHERE B.CONTENT LIKE vStatus;<br />
                    --order by tad,RM_NUMBER asc;<br />
                    else<br />
                    delete from BM_IBPS02_TEMP;<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    upper(trim(A.CCYCD)),<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    --decode(pCitad,&#39;ALL&#39;,&#39;ALL&#39;, substr(BSEND.SIBS_BANK_CODE,-3) || &#39; - &#39; || 
                    BSEND.BANK_NAME) AS BRAN_NAME,
                    <br />
                    vBranchName,<br />
                    A.Tellerid,<br />
                    BR.Gw_Bank_Code || &#39; - &#39; || BR.Tad_Name AS TADNAME, -- tad name<br />
                    BR1.SIBS_BANK_CODE || &#39; - &#39; || BR1.BRAN_NAME AS PRETADNAME -- pre tad name<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.Tellerid,<br />
                    C.F21<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND nvl(C.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    /*AND C.F07 LIKE vSender
                    <br />
                    AND lpad(C.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch*/<br />
                    <br />
                    AND ((nvl(C.Product_Type, &#39;%&#39;) = &#39;OL3&#39; AND<br />
                    (lpad(C.Tad, 5, &#39;0&#39;) like vSrcBranch OR<br />
                    (C.F07 LIKE vSender AND<br />
                    lpad(C.Source_Branch, 5, &#39;0&#39;) LIKE vSrcBranch))) OR<br />
                    (nvl(C.Product_Type, &#39;%&#39;) &lt;&gt; &#39;OL3&#39; AND<br />
                    C.F07 LIKE vSender AND<br />
                    lpad(C.Source_Branch, 5, &#39;0&#39;) LIKE vSrcBranch))) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(A.STATUS) = trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND<br />
                    on BSEND.gw_bank_code = A.SENDER<br />
                    AND BSEND.Sibs_Bank_Code =<br />
                    decode(BSEND.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV<br />
                    on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND BRECV.Sibs_Bank_Code =<br />
                    decode(BRECV.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    --LEFT JOIN IBPS_BANK_MAP BR ON BR.Gw_Bank_Code = (A.F21)
                    <br />
                    -- AND BR.Sibs_Bank_Code=decode(BR.Sibs_Bank_Code,-1,-1,lpad(A.TAD,5,&#39;0&#39;))
                    <br />
                    LEFT JOIN TAD BR<br />
                    ON BR.Gw_Bank_Code = A.F21<br />
                    LEFT JOIN BRANCH BR1<br />
                    ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)<br />
                    WHERE B.CONTENT LIKE vStatus;<br />
                    --order by tad,RM_NUMBER asc;<br />
                    --Lay du lieu trong thang<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    upper(trim(A.CCYCD)),<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    --decode(pCitad,&#39;ALL&#39;,&#39;ALL&#39;, substr(BSEND.SIBS_BANK_CODE,-3) || &#39; - &#39; || 
                    BSEND.BANK_NAME) AS BRAN_NAME,
                    <br />
                    vBranchName,<br />
                    A.Tellerid,<br />
                    BR.Gw_Bank_Code || &#39; - &#39; || BR.Tad_Name AS TADNAME, -- tad name<br />
                    BR1.SIBS_BANK_CODE || &#39; - &#39; || BR1.BRAN_NAME AS PRETADNAME -- pre tad name<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.Tellerid,<br />
                    C.F21<br />
                    FROM IBPS_MSG_ALL C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND nvl(C.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    /*AND C.F07 LIKE vSender
                    <br />
                    AND lpad(C.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch*/<br />
                    <br />
                    AND ((nvl(C.Product_Type, &#39;%&#39;) = &#39;OL3&#39; AND<br />
                    (lpad(C.Tad, 5, &#39;0&#39;) like vSrcBranch OR<br />
                    (C.F07 LIKE vSender AND<br />
                    lpad(C.Source_Branch, 5, &#39;0&#39;) LIKE vSrcBranch))) OR<br />
                    (nvl(C.Product_Type, &#39;%&#39;) &lt;&gt; &#39;OL3&#39; AND<br />
                    C.F07 LIKE vSender AND<br />
                    lpad(C.Source_Branch, 5, &#39;0&#39;) LIKE vSrcBranch))) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(A.STATUS) = trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND<br />
                    on BSEND.gw_bank_code = A.SENDER<br />
                    AND BSEND.Sibs_Bank_Code =<br />
                    decode(BSEND.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV<br />
                    on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND BRECV.Sibs_Bank_Code =<br />
                    decode(BRECV.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    --LEFT JOIN IBPS_BANK_MAP BR ON BR.Gw_Bank_Code = (A.F21)
                    <br />
                    -- AND BR.Sibs_Bank_Code=decode(BR.Sibs_Bank_Code,-1,-1,lpad(A.TAD,5,&#39;0&#39;))
                    <br />
                    LEFT JOIN TAD BR<br />
                    ON BR.Gw_Bank_Code = A.F21<br />
                    LEFT JOIN BRANCH BR1<br />
                    ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)<br />
                    <br />
                    WHERE B.CONTENT LIKE vStatus;<br />
                    --Lay du lieu trong nam<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    upper(trim(A.CCYCD)),<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    --decode(pCitad,&#39;ALL&#39;,&#39;ALL&#39;, substr(BSEND.SIBS_BANK_CODE,-3) || &#39; - &#39; || 
                    BSEND.BANK_NAME) AS BRAN_NAME,
                    <br />
                    vBranchName,<br />
                    A.Tellerid,<br />
                    BR.Gw_Bank_Code || &#39; - &#39; || BR.Tad_Name AS TADNAME, -- tad name<br />
                    BR1.SIBS_BANK_CODE || &#39; - &#39; || BR1.BRAN_NAME AS PRETADNAME -- pre tad name<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.Tellerid,<br />
                    C.F21<br />
                    FROM IBPS_MSG_ALL_HIS C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND nvl(C.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    /*AND C.F07 LIKE vSender
                    <br />
                    AND lpad(C.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch*/<br />
                    <br />
                    AND ((nvl(C.Product_Type, &#39;%&#39;) = &#39;OL3&#39; AND<br />
                    (lpad(C.Tad, 5, &#39;0&#39;) like vSrcBranch OR<br />
                    (C.F07 LIKE vSender AND<br />
                    lpad(C.Source_Branch, 5, &#39;0&#39;) LIKE vSrcBranch))) OR<br />
                    (nvl(C.Product_Type, &#39;%&#39;) &lt;&gt; &#39;OL3&#39; AND<br />
                    C.F07 LIKE vSender AND<br />
                    lpad(C.Source_Branch, 5, &#39;0&#39;) LIKE vSrcBranch))) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(A.STATUS) = trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND<br />
                    on BSEND.gw_bank_code = A.SENDER<br />
                    AND BSEND.Sibs_Bank_Code =<br />
                    decode(BSEND.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV<br />
                    on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND BRECV.Sibs_Bank_Code =<br />
                    decode(BRECV.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    --LEFT JOIN IBPS_BANK_MAP BR ON BR.Gw_Bank_Code = (A.F21)
                    <br />
                    -- AND BR.Sibs_Bank_Code=decode(BR.Sibs_Bank_Code,-1,-1,lpad(A.TAD,5,&#39;0&#39;))
                    <br />
                    LEFT JOIN TAD BR<br />
                    ON BR.Gw_Bank_Code = A.F21<br />
                    LEFT JOIN BRANCH BR1<br />
                    ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)<br />
                    WHERE B.CONTENT LIKE vStatus;<br />
                    end if;<br />
                    <br />
                    -- get from backup
                    <br />
                    delete from BM_IBPS02_TEMP<br />
                    where query_id in (select query_id<br />
                    from ibps_msg_content_backup C<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(C.STATUS) = trim(B.STATUS)<br />
                    where to_char(TRANS_DATE, &#39;YYYYMMDD&#39;) =<br />
                    to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    and to_char(create_Date, &#39;YYYYMMDD&#39;) &lt;&gt;<br />
                    to_char(C.TRANS_DATE, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND nvl(C.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    AND ((nvl(C.Product_Type, &#39;%&#39;) = &#39;OL3&#39; AND<br />
                    (lpad(C.Tad, 5, &#39;0&#39;) like vSrcBranch OR<br />
                    (C.F07 LIKE vSender AND<br />
                    lpad(C.Source_Branch, 5, &#39;0&#39;) LIKE vSrcBranch))) OR<br />
                    (nvl(C.Product_Type, &#39;%&#39;) &lt;&gt; &#39;OL3&#39; AND<br />
                    C.F07 LIKE vSender AND<br />
                    lpad(C.Source_Branch, 5, &#39;0&#39;) LIKE vSrcBranch))<br />
                    );<br />
                    <br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    upper(trim(A.CCYCD)),<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    --decode(pCitad,&#39;ALL&#39;,&#39;ALL&#39;, substr(BSEND.SIBS_BANK_CODE,-3) || &#39; - &#39; || 
                    BSEND.BANK_NAME) AS BRAN_NAME,
                    <br />
                    vBranchName,<br />
                    A.Tellerid,<br />
                    BR.Gw_Bank_Code || &#39; - &#39; || BR.Tad_Name AS TADNAME, -- tad name<br />
                    BR1.SIBS_BANK_CODE || &#39; - &#39; || BR1.BRAN_NAME AS PRETADNAME -- pre tad name<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.Tellerid,<br />
                    C.F21<br />
                    FROM IBPS_MSG_Content_backup C<br />
                    WHERE
                    <br />
                    C.Id IN
                    <br />
                    (Select Max(C1.ID) from IBPS_MSG_Content_backup C1
                    <br />
                    where C1.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    and to_char(C1.create_Date,&#39;YYYYMMDD&#39;)&lt;&gt;to_char(C1.TRANS_DATE, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C1.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND nvl(C1.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    AND ((nvl(C1.Product_Type, &#39;%&#39;) = &#39;OL3&#39; AND<br />
                    (lpad(C1.Tad, 5, &#39;0&#39;) like vSrcBranch OR<br />
                    (C1.F07 LIKE vSender AND<br />
                    lpad(C1.Source_Branch, 5, &#39;0&#39;) LIKE vSrcBranch))) OR<br />
                    (nvl(C1.Product_Type, &#39;%&#39;) &lt;&gt; &#39;OL3&#39; AND<br />
                    C1.F07 LIKE vSender AND<br />
                    lpad(C1.Source_Branch, 5, &#39;0&#39;) LIKE vSrcBranch))<br />
                    group by C1.Query_Id<br />
                    )<br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(A.STATUS) = trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND<br />
                    on BSEND.gw_bank_code = A.SENDER<br />
                    AND BSEND.Sibs_Bank_Code =<br />
                    decode(BSEND.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV<br />
                    on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND BRECV.Sibs_Bank_Code =<br />
                    decode(BRECV.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    --LEFT JOIN IBPS_BANK_MAP BR ON BR.Gw_Bank_Code = (A.F21)
                    <br />
                    -- AND BR.Sibs_Bank_Code=decode(BR.Sibs_Bank_Code,-1,-1,lpad(A.TAD,5,&#39;0&#39;))
                    <br />
                    LEFT JOIN TAD BR<br />
                    ON BR.Gw_Bank_Code = A.F21<br />
                    LEFT JOIN BRANCH BR1<br />
                    ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)<br />
                    WHERE B.CONTENT LIKE vStatus;<br />
                    <br />
                    open RefCurBM_IBPS02_CN for<br />
                    select *<br />
                    from BM_IBPS02_TEMP<br />
                    order by CCYCD, TAD, GW_TRANS_NUM, RM_NUMBER asc;<br />
                    END MSB_BM_IBPS_02_CN;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;MSB_BM_IBPS_04&quot; (pDate in date,<br />
                    pCitadOld in varchar2, -- ma 8 so<br />
                    pCitadNew in varchar2, -- ma 8 so<br />
                    pCcycd in varchar2 default &#39;VND&#39;,<br />
                    pSTATUS IN VARCHAR2,<br />
                    RefCurBM_IBPS04 IN OUT PKG_CR.RefCurType) IS<br />
                    --vDate date;<br />
                    vCitadOld varchar2(50); --ma 3 so<br />
                    vCitadNew varchar2(50);-- ma 8 so<br />
                    vCcycd varchar2(3);<br />
                    vOndate date;<br />
                    vStatus varchar2(20);<br />
                    BEGIN<br />
                    select sysdate into vOndate from dual;<br />
                    if upper(trim(pCitadOld)) = &#39;ALL&#39; or trim(pCitadOld) IS NULL then<br />
                    vCitadOld := &#39;%&#39;;<br />
                    else<br />
                    select nvl(T.Sibs_Code,&#39;&#39;) into vCitadOld
                    <br />
                    from tad T where T.Gw_Bank_Code LIKE<br />
                    trim(pCitadOld) AND rownum=1;<br />
                    end if;<br />
                    if upper(trim(pCitadNew)) = &#39;ALL&#39; or trim(pCitadNew) IS NULL then<br />
                    vCitadNew := &#39;%&#39;;<br />
                    else
                    <br />
                    vCitadNew := trim(pCitadNew);<br />
                    end if;<br />
                    if upper(trim(pCcycd)) = &#39;ALL&#39; or trim(pCcycd) IS NULL then<br />
                    vCcycd := &#39;%&#39;;<br />
                    else<br />
                    vCcycd := trim(pCcycd);<br />
                    end if;<br />
                    if upper(trim(pStatus)) = &#39;ALL&#39; or trim(pStatus) IS NULL then<br />
                    vStatus := &#39;%&#39;;<br />
                    else<br />
                    vStatus := trim(pStatus);<br />
                    end if;<br />
                    <br />
                    if to_char(pDate, &#39;DDMMYYYY&#39;)= to_char(vOndate, &#39;DDMMYYYY&#39;) then<br />
                    --open RefCurBM_IBPS02 for<br />
                    delete from BM_IBPS02_TEMP;<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    TRAN_CODE,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    decode(vCitadOld,&#39;%&#39;,&#39;ALL&#39;, BR1.GW_BANK_CODE || &#39; - &#39; || BR1.Tad_Name),
                    <br />
                    A.Tellerid,<br />
                    T.GW_BANK_CODE || &#39; - &#39; || T.Bank_Name, -- tad name<br />
                    BR1.TAD_NAME -- pre tad name<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS BUTTOAN,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;011&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid,<br />
                    C.F21<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    where trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    --AND C.PRE_TAD IS NOT NULL<br />
                    AND lpad(C.PRE_TAD,5,&#39;0&#39;) like vCitadOld<br />
                    --and C.tad like vCitadNew<br />
                    AND C.F21 LIKE vCitadNew<br />
                    AND length(C.GW_TRANS_NUM)=6<br />
                    AND substr(C.Gw_Trans_Num,1,1)=&#39;6&#39;<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.TRANSDATE = To_char(pDATE, &#39;YYYYMMDD&#39;)) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BR ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN TAD BR1 ON BR1.Sibs_Code =<br />
                    lpad(A.Pre_Tad, 5, &#39;0&#39;)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    WHERE B.CONTENT LIKE vStatus<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    elsif substr(to_char(pDate, &#39;DDMMYYYY&#39;), 3, 6) =<br />
                    substr(to_char(vOndate, &#39;DDMMYYYY&#39;),<br />
                    3,<br />
                    6) then<br />
                    delete from BM_IBPS02_TEMP;<br />
                    --Lay du lieu trong ngay<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    decode(vCitadOld,&#39;%&#39;,&#39;ALL&#39;, BR1.GW_BANK_CODE || &#39; - &#39; || BR1.Tad_Name),
                    <br />
                    A.Tellerid,<br />
                    T.GW_BANK_CODE || &#39; - &#39; || T.Bank_Name, -- tad name<br />
                    BR1.TAD_NAME -- pre tad name<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,
                    <br />
                    C.Tellerid,<br />
                    C.F21
                    <br />
                    FROM IBPS_MSG_CONTENT C<br />
                    where trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND C.PRE_TAD IS NOT NULL<br />
                    AND lpad(C.PRE_TAD,5,&#39;0&#39;) like vCitadOld<br />
                    --and C.tad like vCitadNew<br />
                    AND C.F21 LIKE vCitadNew<br />
                    AND length(C.GW_TRANS_NUM)=6<br />
                    AND substr(C.Gw_Trans_Num,1,1)=&#39;6&#39;<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.TRANSDATE = To_char(pDATE, &#39;YYYYMMDD&#39;)) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BR ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN TAD BR1 ON BR1.Sibs_Code =<br />
                    lpad(A.Pre_Tad, 5, &#39;0&#39;)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    WHERE B.CONTENT LIKE vStatus<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --Lay du lieu trong thang<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    decode(vCitadOld,&#39;%&#39;,&#39;ALL&#39;, BR1.GW_BANK_CODE || &#39; - &#39; || BR1.Tad_Name),
                    <br />
                    A.Tellerid,<br />
                    T.GW_BANK_CODE || &#39; - &#39; || T.Bank_Name, -- tad name<br />
                    BR1.TAD_NAME -- pre tad name<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid,<br />
                    C.F21<br />
                    FROM IBPS_MSG_ALL C<br />
                    where trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND C.PRE_TAD IS NOT NULL<br />
                    AND lpad(C.PRE_TAD,5,&#39;0&#39;) like vCitadOld<br />
                    --and C.tad like vCitadNew<br />
                    AND C.F21 LIKE vCitadNew<br />
                    AND length(C.GW_TRANS_NUM)=6<br />
                    AND substr(C.Gw_Trans_Num,1,1)=&#39;6&#39;<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.TRANSDATE = To_char(pDATE, &#39;YYYYMMDD&#39;)) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BR ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN TAD BR1 ON BR1.Sibs_Code =<br />
                    lpad(A.Pre_Tad, 5, &#39;0&#39;)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    WHERE B.CONTENT LIKE vStatus<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --order by tad,RM_NUMBER asc;<br />
                    else<br />
                    delete from BM_IBPS02_TEMP;<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    decode(vCitadOld,&#39;%&#39;,&#39;ALL&#39;, BR1.GW_BANK_CODE || &#39; - &#39; || BR1.Tad_Name),
                    <br />
                    A.TelleriD,<br />
                    T.GW_BANK_CODE || &#39; - &#39; || T.Bank_Name, -- tad name<br />
                    BR1.TAD_NAME -- pre tad name<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.TellerID,<br />
                    C.F21<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    where trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND C.PRE_TAD IS NOT NULL<br />
                    AND lpad(C.PRE_TAD,5,&#39;0&#39;) like vCitadOld<br />
                    --and C.tad like vCitadNew<br />
                    AND C.F21 LIKE vCitadNew<br />
                    AND length(C.GW_TRANS_NUM)=6<br />
                    AND substr(C.Gw_Trans_Num,1,1)=&#39;6&#39;<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.TRANSDATE = To_char(pDATE, &#39;YYYYMMDD&#39;)) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BR ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN TAD BR1 ON BR1.Sibs_Code =<br />
                    lpad(A.Pre_Tad, 5, &#39;0&#39;)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    WHERE B.CONTENT LIKE vStatus<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --order by tad,RM_NUMBER asc;<br />
                    --Lay du lieu trong thang<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    decode(vCitadOld,&#39;%&#39;,&#39;ALL&#39;, BR1.GW_BANK_CODE || &#39; - &#39; || BR1.Tad_Name),
                    <br />
                    A.TellerID,<br />
                    T.GW_BANK_CODE || &#39; - &#39; || T.Bank_Name, -- tad name<br />
                    BR1.TAD_NAME -- pre tad name<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.TellerID,<br />
                    C.F21<br />
                    FROM IBPS_MSG_ALL C<br />
                    where trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND C.PRE_TAD IS NOT NULL<br />
                    AND lpad(C.PRE_TAD,5,&#39;0&#39;) like vCitadOld<br />
                    --and C.tad like vCitadNew<br />
                    AND C.F21 LIKE vCitadNew<br />
                    AND length(C.GW_TRANS_NUM)=6<br />
                    AND substr(C.Gw_Trans_Num,1,1)=&#39;6&#39;<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.TRANSDATE = To_char(pDATE, &#39;YYYYMMDD&#39;)) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BR ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN TAD BR1 ON BR1.Sibs_Code =<br />
                    lpad(A.Pre_Tad, 5, &#39;0&#39;)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    WHERE B.CONTENT LIKE vStatus<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --Lay du lieu trong nam<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    decode(vCitadOld,&#39;%&#39;,&#39;ALL&#39;, BR1.GW_BANK_CODE || &#39; - &#39; || BR1.Tad_Name),
                    <br />
                    A.TellerID,<br />
                    T.GW_BANK_CODE || &#39; - &#39; || T.Bank_Name, -- tad name<br />
                    BR1.TAD_NAME -- pre tad name<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.TellerID,<br />
                    C.F21<br />
                    FROM IBPS_MSG_ALL_HIS C<br />
                    where trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND C.PRE_TAD IS NOT NULL<br />
                    AND lpad(C.PRE_TAD,5,&#39;0&#39;) like vCitadOld<br />
                    --and C.tad like vCitadNew<br />
                    AND C.F21 LIKE vCitadNew<br />
                    AND length(C.GW_TRANS_NUM)=6<br />
                    AND substr(C.Gw_Trans_Num,1,1)=&#39;6&#39;<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.TRANSDATE = To_char(pDATE, &#39;YYYYMMDD&#39;)) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BR ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN TAD BR1 ON BR1.Sibs_Code =<br />
                    lpad(A.Pre_Tad, 5, &#39;0&#39;)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    WHERE B.CONTENT LIKE vStatus<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    end if;<br />
                    <br />
                    -- get from backup
                    <br />
                    delete from BM_IBPS02_TEMP<br />
                    where query_id in (select query_id<br />
                    from ibps_msg_content_backup C<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(C.STATUS) = trim(B.STATUS)<br />
                    where to_char(TRANS_DATE, &#39;YYYYMMDD&#39;) =<br />
                    to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    and to_char(create_Date, &#39;YYYYMMDD&#39;) &lt;&gt;<br />
                    to_char(C.TRANS_DATE, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND C.PRE_TAD IS NOT NULL<br />
                    AND lpad(C.PRE_TAD,5,&#39;0&#39;) like vCitadOld<br />
                    --and C.tad like vCitadNew<br />
                    AND C.F21 LIKE vCitadNew<br />
                    AND length(C.GW_TRANS_NUM)=6<br />
                    AND substr(C.Gw_Trans_Num,1,1)=&#39;6&#39;<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    );<br />
                    <br />
                    <br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, --tad name<br />
                    EXTFIELD03 -- pre tad name<br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    decode(vCitadOld,&#39;%&#39;,&#39;ALL&#39;, BR1.GW_BANK_CODE || &#39; - &#39; || BR1.Tad_Name),
                    <br />
                    A.TellerID,<br />
                    T.GW_BANK_CODE || &#39; - &#39; || T.Bank_Name, -- tad name<br />
                    BR1.TAD_NAME -- pre tad name<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.TellerID,<br />
                    C.F21<br />
                    FROM IBPS_MSG_Content_backup C<br />
                    WHERE
                    <br />
                    C.Id IN
                    <br />
                    (Select Max(C1.ID) from IBPS_MSG_Content_backup C1
                    <br />
                    where C1.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    and to_char(C1.create_Date,&#39;YYYYMMDD&#39;)&lt;&gt;to_char(C1.TRANS_DATE, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C1.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    AND C1.PRE_TAD IS NOT NULL<br />
                    AND lpad(C1.PRE_TAD,5,&#39;0&#39;) like vCitadOld<br />
                    --and C.tad like vCitadNew<br />
                    AND C1.F21 LIKE vCitadNew<br />
                    AND length(C1.GW_TRANS_NUM)=6<br />
                    AND substr(C1.Gw_Trans_Num,1,1)=&#39;6&#39;<br />
                    AND nvl(C1.ccycd,&#39;%&#39;) LIKE vCcycd
                    <br />
                    group by C1.Query_Id)<br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BR ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN TAD BR1 ON BR1.Sibs_Code =<br />
                    lpad(A.Pre_Tad, 5, &#39;0&#39;)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    WHERE B.CONTENT LIKE vStatus ;
                    <br />
                    <br />
                    <br />
                    <br />
                    -----<br />
                    open RefCurBM_IBPS04 for<br />
                    select * from bm_ibps02_temp order by GW_TRANS_NUM;<br />
                    END MSB_BM_IBPS_04;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;MSB_BM_IBPS_05&quot; (pDATE IN DATE,<br />
                    pCitad IN VARCHAR2, -- citad ma 8 so<br />
                    pCcycd IN VARCHAR2,<br />
                    RefCurBM_IBPS05 IN OUT PKG_CR.RefCurType) IS<br />
                    <br />
                    vCitad VARCHAR2(10);<br />
                    vCcycd VARCHAR2(10);<br />
                    vOndate date;<br />
                    BEGIN<br />
                    select sysdate into vOndate from dual;<br />
                    if upper(trim(pCitad)) = &#39;ALL&#39; or trim(pCitad) is null then<br />
                    vCitad := &#39;%&#39;;<br />
                    else<br />
                    vCitad := trim(pCitad);<br />
                    end if;<br />
                    if upper(trim(pCcycd)) = &#39;ALL&#39; or trim(pCcycd) is null then<br />
                    vCcycd := &#39;%&#39;;<br />
                    else<br />
                    vCcycd := trim(pCcycd);<br />
                    end if;<br />
                    <br />
                    if to_char(pDate, &#39;DDMMYYYY&#39;) = to_char(vOndate, &#39;DDMMYYYY&#39;) then<br />
                    DELETE FROM BM_IBPS02_TEMP;<br />
                    --Du lieu trong ngay<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    TRAN_CODE,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01, -- pre tad name<br />
                    EXTFIELD02 --tad name
                    <br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    decode(vCitad,<br />
                    &#39;%&#39;,<br />
                    &#39;ALL&#39;,<br />
                    T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    BR1.Bran_Name, -- pre tad name<br />
                    BSEND.GW_BANK_CODE || &#39; - &#39; || BSEND.BANK_NAME --chi nhanh nhan dien F07
                    <br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS BUTTOAN,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;011&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.F21<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 like vCitad<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) = &#39;7&#39;) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(A.STATUS) = trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND<br />
                    on BSEND.gw_bank_code = A.SENDER<br />
                    AND BSEND.Sibs_Bank_Code =<br />
                    decode(BSEND.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV<br />
                    on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND BRECV.Sibs_Bank_Code =<br />
                    decode(BRECV.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN BRANCH BR<br />
                    ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN BRANCH BR1<br />
                    ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T<br />
                    ON T.GW_BANK_CODE = A.F21<br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE);<br />
                    <br />
                    elsif substr(to_char(pDate, &#39;DDMMYYYY&#39;), 3, 6) =<br />
                    substr(to_char(vOndate, &#39;DDMMYYYY&#39;), 3, 6) then<br />
                    delete from BM_IBPS02_TEMP;<br />
                    --Lay du lieu trong ngay<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01, -- pre tad name<br />
                    EXTFIELD02 --tad name
                    <br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    decode(vCitad,<br />
                    &#39;%&#39;,<br />
                    &#39;ALL&#39;,<br />
                    T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    BR1.Bran_Name, -- pre tad name<br />
                    BSEND.GW_BANK_CODE || &#39; - &#39; || BSEND.BANK_NAME --chi nhanh nhan dien F07<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.F21<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 like vCitad<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) = &#39;7&#39;) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(A.STATUS) = trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND<br />
                    on BSEND.gw_bank_code = A.SENDER<br />
                    AND BSEND.Sibs_Bank_Code =<br />
                    decode(BSEND.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV<br />
                    on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND BRECV.Sibs_Bank_Code =<br />
                    decode(BRECV.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    <br />
                    LEFT JOIN BRANCH BR<br />
                    ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN BRANCH BR1<br />
                    ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T<br />
                    ON T.GW_BANK_CODE = A.F21<br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE)<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --Lay du lieu trong thang<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01, -- pre tad name<br />
                    EXTFIELD02 --tad name
                    <br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    decode(vCitad,<br />
                    &#39;%&#39;,<br />
                    &#39;ALL&#39;,<br />
                    T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    BR1.Bran_Name, -- pre tad name<br />
                    BSEND.GW_BANK_CODE || &#39; - &#39; || BSEND.BANK_NAME --chi nhanh nhan dien F07<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.F21<br />
                    FROM IBPS_MSG_ALL C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 like vCitad<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) = &#39;7&#39;) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(A.STATUS) = trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND<br />
                    on BSEND.gw_bank_code = A.SENDER<br />
                    AND BSEND.Sibs_Bank_Code =<br />
                    decode(BSEND.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV<br />
                    on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND BRECV.Sibs_Bank_Code =<br />
                    decode(BRECV.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    <br />
                    LEFT JOIN BRANCH BR<br />
                    ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN BRANCH BR1<br />
                    ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T<br />
                    ON T.GW_BANK_CODE = A.F21<br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE);<br />
                    --order by A.Trans_Code, RECEIVER asc;<br />
                    --order by tad,RM_NUMBER asc;<br />
                    else<br />
                    delete from BM_IBPS02_TEMP;<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01, -- pre tad name<br />
                    EXTFIELD02 --tad name
                    <br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    decode(vCitad,<br />
                    &#39;%&#39;,<br />
                    &#39;ALL&#39;,<br />
                    T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    BR1.Bran_Name, -- pre tad name<br />
                    BSEND.GW_BANK_CODE || &#39; - &#39; || BSEND.BANK_NAME --chi nhanh nhan dien F07<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.F21<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 like vCitad<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) = &#39;7&#39;) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(A.STATUS) = trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND<br />
                    on BSEND.gw_bank_code = A.SENDER<br />
                    AND BSEND.Sibs_Bank_Code =<br />
                    decode(BSEND.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV<br />
                    on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND BRECV.Sibs_Bank_Code =<br />
                    decode(BRECV.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN BRANCH BR<br />
                    ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN BRANCH BR1<br />
                    ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T<br />
                    ON T.GW_BANK_CODE = A.F21<br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE);<br />
                    --order by A.Trans_Code, RECEIVER asc;<br />
                    --order by tad,RM_NUMBER asc;<br />
                    --Lay du lieu trong thang<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01, -- pre tad name<br />
                    EXTFIELD02 --tad name
                    <br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    decode(vCitad,<br />
                    &#39;%&#39;,<br />
                    &#39;ALL&#39;,<br />
                    T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    BR1.Bran_Name, -- pre tad name<br />
                    BSEND.GW_BANK_CODE || &#39; - &#39; || BSEND.BANK_NAME --chi nhanh nhan dien F07<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.F21<br />
                    FROM IBPS_MSG_ALL C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd, &#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 like vCitad<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) = &#39;7&#39;) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(A.STATUS) = trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND<br />
                    on BSEND.gw_bank_code = A.SENDER<br />
                    AND BSEND.Sibs_Bank_Code =<br />
                    decode(BSEND.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV<br />
                    on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND BRECV.Sibs_Bank_Code =<br />
                    decode(BRECV.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN BRANCH BR<br />
                    ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN BRANCH BR1<br />
                    ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T<br />
                    ON T.GW_BANK_CODE = A.F21<br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE);<br />
                    -- order by A.Trans_Code, RECEIVER asc;<br />
                    --Lay du lieu trong nam<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01, -- pre tad name<br />
                    EXTFIELD02 --tad name
                    <br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    decode(vCitad,<br />
                    &#39;%&#39;,<br />
                    &#39;ALL&#39;,<br />
                    T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    BR1.Bran_Name, -- pre tad name<br />
                    BSEND.GW_BANK_CODE || &#39; - &#39; || BSEND.BANK_NAME --chi nhanh nhan dien F07<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.F21<br />
                    FROM IBPS_MSG_ALL_HIS C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND C.ccycd LIKE vCcycd<br />
                    AND C.F21 like vCitad<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) = &#39;7&#39;) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(A.STATUS) = trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND<br />
                    on BSEND.gw_bank_code = A.SENDER<br />
                    AND BSEND.Sibs_Bank_Code =<br />
                    decode(BSEND.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV<br />
                    on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND BRECV.Sibs_Bank_Code =<br />
                    decode(BRECV.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN BRANCH BR<br />
                    ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN BRANCH BR1<br />
                    ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T<br />
                    ON T.GW_BANK_CODE = A.F21<br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE);<br />
                    end if;<br />
                    <br />
                    -- get from backup<br />
                    delete from BM_IBPS02_TEMP<br />
                    where query_id in (select query_id<br />
                    from ibps_msg_content_backup C<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(C.STATUS) = trim(B.STATUS)<br />
                    where to_char(TRANS_DATE, &#39;YYYYMMDD&#39;) =<br />
                    to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    and to_char(create_Date, &#39;YYYYMMDD&#39;) &lt;&gt;<br />
                    to_char(C.TRANS_DATE, &#39;YYYYMMDD&#39;)
                    <br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND nvl(C.F21,&#39;%&#39;) like vCitad<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) = &#39;7&#39;);<br />
                    <br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01, -- pre tad name<br />
                    EXTFIELD02 --tad name
                    <br />
                    )<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    decode(vCitad,<br />
                    &#39;%&#39;,<br />
                    &#39;ALL&#39;,<br />
                    T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    BR1.Bran_Name, -- pre tad name<br />
                    BSEND.GW_BANK_CODE || &#39; - &#39; || BSEND.BANK_NAME --chi nhanh nhan dien F07<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.F21<br />
                    FROM ibps_msg_content_backup C<br />
                    WHERE
                    <br />
                    C.Id IN
                    <br />
                    (Select Max(C1.ID) from IBPS_MSG_Content_backup C1
                    <br />
                    where C1.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    and to_char(C1.create_Date,&#39;YYYYMMDD&#39;)&lt;&gt;to_char(C1.TRANS_DATE, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C1.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND nvl(C1.F21,&#39;%&#39;) like vCitad<br />
                    AND LENGTH(TO_CHAR(C1.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C1.GW_TRANS_NUM), 1, 1) = &#39;7&#39;<br />
                    group by C1.Query_Id)<br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(A.STATUS) = trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND<br />
                    on BSEND.gw_bank_code = A.SENDER<br />
                    AND BSEND.Sibs_Bank_Code =<br />
                    decode(BSEND.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV<br />
                    on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND BRECV.Sibs_Bank_Code =<br />
                    decode(BRECV.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(A.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN BRANCH BR<br />
                    ON BR.SIBS_BANK_CODE = SUBSTR(A.TAD, 3, 3)<br />
                    LEFT JOIN BRANCH BR1<br />
                    ON BR1.SIBS_BANK_CODE = SUBSTR(A.Pre_Tad, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T<br />
                    ON T.GW_BANK_CODE = A.F21<br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE);<br />
                    <br />
                    <br />
                    open RefCurBM_IBPS05 for select * from BM_IBPS02_TEMP order by GW_TRANS_NUM, 
                    RM_NUMBER asc;<br />
                    <br />
                    END MSB_BM_IBPS_05;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;MSB_BM_IBPS_06&quot; (pDate in date,<br />
                    pCitad in varchar2, -- ma 8 so<br />
                    pCcycd in varchar2 default &#39;VND&#39;,<br />
                    pStatus in varchar2,<br />
                    RefCurBM_IBPS06 IN OUT PKG_CR.RefCurType) IS<br />
                    --vDate date;<br />
                    vCitad varchar2(50);<br />
                    vCcycd varchar2(3);<br />
                    vStatus varchar2(50);<br />
                    vOndate date;<br />
                    vBranchName varchar2(200);<br />
                    BEGIN<br />
                    <br />
                    select sysdate into vOndate from dual;<br />
                    if upper(trim(pCitad)) = &#39;ALL&#39; or trim(pCitad) is null then<br />
                    vCitad := &#39;%&#39;;<br />
                    vBranchName:=&#39;ALL&#39;;<br />
                    else<br />
                    vCitad := trim(pCitad);<br />
                    select substr(T.Sibs_Code,-3) || &#39;-&#39; || T.Tad_Name into vBranchName from tad T 
                    where T.Gw_Bank_Code=pCitad and rownum=1;<br />
                    <br />
                    end if;<br />
                    if upper(trim(pCcycd)) = &#39;ALL&#39; or trim(pCcycd) is null then<br />
                    vCcycd := &#39;%&#39;;<br />
                    else<br />
                    vCcycd := trim(pCcycd);<br />
                    end if;<br />
                    if upper(trim(pStatus)) = &#39;ALL&#39; or trim(pStatus) is null then<br />
                    vStatus := &#39;%&#39;;<br />
                    else<br />
                    vStatus := trim(pStatus);<br />
                    end if;<br />
                    <br />
                    if to_char(pDate, &#39;DDMMYYYY&#39;)= to_char(vOndate, &#39;DDMMYYYY&#39;) then<br />
                    --open RefCurBM_IBPS02 for<br />
                    delete from BM_IBPS02_TEMP;<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    TRAN_CODE,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    --decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,
                    <br />
                    A.Tellerid<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS BUTTOAN,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;011&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F21 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid,<br />
                    C.F21,<br />
                    C.F22<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;IBPS-SIBS&#39;<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F22 like vCitad) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F22<br />
                    AND T.Sibs_Bank_Code&lt;&gt;-1
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    WHERE B.CONTENT LIKE vStatus<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    elsif substr(to_char(pDate, &#39;DDMMYYYY&#39;), 3, 6) =<br />
                    substr(to_char(vOndate, &#39;DDMMYYYY&#39;),<br />
                    3,<br />
                    6) then<br />
                    delete from BM_IBPS02_TEMP;<br />
                    --Lay du lieu trong ngay<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    --decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,
                    <br />
                    A.Tellerid<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F21 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid,<br />
                    C.F21,<br />
                    C.F22<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;IBPS-SIBS&#39;<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F22 like vCitad) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F22<br />
                    AND T.Sibs_Bank_Code&lt;&gt;-1
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    WHERE B.CONTENT LIKE vStatus<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --Lay du lieu trong thang<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    -- decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,
                    <br />
                    A.Tellerid<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F21 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid,<br />
                    C.F21,<br />
                    C.F22<br />
                    FROM IBPS_MSG_ALL C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;IBPS-SIBS&#39;<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F22 like vCitad) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F22<br />
                    AND T.Sibs_Bank_Code&lt;&gt;-1
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    WHERE B.CONTENT LIKE vStatus<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --order by tad,RM_NUMBER asc;<br />
                    else<br />
                    delete from BM_IBPS02_TEMP;<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    -- decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,
                    <br />
                    A.TelleriD<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F21 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.TellerID,<br />
                    C.F21,<br />
                    C.F22<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;IBPS-SIBS&#39;<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F22 like vCitad) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F22<br />
                    AND T.Sibs_Bank_Code&lt;&gt;-1
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    WHERE B.CONTENT LIKE vStatus<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --order by tad,RM_NUMBER asc;<br />
                    --Lay du lieu trong thang<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    --decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,
                    <br />
                    A.TellerID<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F21 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.TellerID,<br />
                    C.F21,<br />
                    C.F22<br />
                    FROM IBPS_MSG_ALL C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;IBPS-SIBS&#39;<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F22 like vCitad) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F22<br />
                    AND T.Sibs_Bank_Code&lt;&gt;-1
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    WHERE B.CONTENT LIKE vStatus<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --Lay du lieu trong nam<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    --decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,
                    <br />
                    A.TellerID<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F21 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.TellerID,<br />
                    C.F21,<br />
                    C.F22<br />
                    FROM IBPS_MSG_ALL_HIS C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;IBPS-SIBS&#39;<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F22 like vCitad) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F22<br />
                    AND T.Sibs_Bank_Code&lt;&gt;-1
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    WHERE B.CONTENT LIKE vStatus<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    end if;<br />
                    <br />
                    -- get from backup
                    <br />
                    delete from BM_IBPS02_TEMP<br />
                    where query_id in (select query_id<br />
                    from ibps_msg_content_backup C<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(C.STATUS) = trim(B.STATUS)<br />
                    where to_char(TRANS_DATE, &#39;YYYYMMDD&#39;) =<br />
                    to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    and to_char(create_Date, &#39;YYYYMMDD&#39;) &lt;&gt;<br />
                    to_char(C.TRANS_DATE, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;IBPS-SIBS&#39;<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F22 like vCitad) ;<br />
                    <br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    --decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,
                    <br />
                    A.TellerID<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F21 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.TellerID,<br />
                    C.F21,<br />
                    C.F22<br />
                    FROM IBPS_MSG_Content_backup C<br />
                    WHERE
                    <br />
                    C.Id IN
                    <br />
                    (Select Max(C1.ID) from IBPS_MSG_Content_backup C1
                    <br />
                    where C1.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    and to_char(C1.create_Date,&#39;YYYYMMDD&#39;)&lt;&gt;to_char(C1.TRANS_DATE, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C1.MSG_DIRECTION) = &#39;IBPS-SIBS&#39;<br />
                    AND nvl(C1.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C1.F22 like vCitad<br />
                    group by C1.Query_Id)<br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F22<br />
                    AND T.Sibs_Bank_Code&lt;&gt;-1
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    WHERE B.CONTENT LIKE vStatus<br />
                    order by A.Trans_Code, RECEIVER asc;
                    <br />
                    <br />
                    open RefCurBM_IBPS06 for<br />
                    select * from BM_IBPS02_TEMP order by GW_TRANS_NUM, RM_NUMBER asc;<br />
                    END MSB_BM_IBPS_06;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;MSB_BM_IBPS_07&quot; (pDATE IN DATE,<br />
                    pCitad IN VARCHAR2, -- citad ma 8 so<br />
                    pBranch IN VARCHAR2, -- ma 8 so<br />
                    pCcycd IN VARCHAR2,<br />
                    RefCurBM_IBPS07 IN OUT PKG_CR.RefCurType) IS<br />
                    <br />
                    vCitad VARCHAR2(10);<br />
                    --vBranch VARCHAR2(10);<br />
                    vCcycd VARCHAR2(10);<br />
                    vOndate date;
                    <br />
                    vSender varchar2(50); --sender 8 so
                    <br />
                    vSrcBranch varchar2(50);
                    <br />
                    vBranchName varchar2(200);
                    <br />
                    BEGIN<br />
                    select sysdate into vOndate from dual;<br />
                    if upper(trim(pBranch)) = &#39;ALL&#39; or trim(pBranch) is null then<br />
                    vSender := &#39;%&#39;;<br />
                    vSrcBranch:=&#39;%&#39;;
                    <br />
                    else<br />
                    GW_PK_IBPS_REPORT.SPLIT_SENDER(trim(pBranch),&#39;-&#39;,vSrcBranch,vSender);<br />
                    vSrcBranch:= lpad(vSrcBranch,5,&#39;0&#39;);<br />
                    <br />
                    end if;
                    <br />
                    if upper(trim(pCitad)) = &#39;ALL&#39; or trim(pCitad) is null then<br />
                    vCitad := &#39;%&#39;;<br />
                    vBranchName:=&#39;ALL&#39;;<br />
                    else<br />
                    vCitad := trim(pCitad);<br />
                    select substr(T.Sibs_Code,-3) || &#39;-&#39; || T.Tad_Name into vBranchName from tad T 
                    where T.Gw_Bank_Code=pCitad and rownum=1;<br />
                    end if;<br />
                    if upper(trim(pCcycd)) = &#39;ALL&#39; or trim(pCcycd) is null then<br />
                    vCcycd := &#39;%&#39;;<br />
                    else<br />
                    vCcycd := trim(pCcycd);<br />
                    end if;<br />
                    /* if upper(trim(pBranch)) = &#39;ALL&#39; or trim(pBranch) is null then<br />
                    vBranch := &#39;%&#39;;<br />
                    else<br />
                    select nvl(T.Sibs_Code,&#39;&#39;) into vBranch
                    <br />
                    from tad T where T.Gw_Bank_Code LIKE<br />
                    trim(pBranch) AND rownum=1;<br />
                    if vBranch&lt;&gt;&#39;&#39; then vBranch:=lpad(vBranch,5,&#39;0&#39;); end if;<br />
                    end if;*/<br />
                    <br />
                    if to_char(pDate, &#39;DDMMYYYY&#39;)= to_char(vOndate, &#39;DDMMYYYY&#39;) then<br />
                    DELETE FROM BM_IBPS02_TEMP;<br />
                    --Du lieu trong ngay<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    TRAN_CODE,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    --decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,<br />
                    A.Tellerid,<br />
                    substr(BSEND.SIBS_BANK_CODE,-3) || &#39;-&#39; || BSEND.Bank_Name AS SRCBRANCHNAME<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS BUTTOAN,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;011&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid,<br />
                    C.F21<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)
                    <br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    --AND C.PRE_TAD IS NOT NULL<br />
                    --AND lpad(C.PRE_TAD,5,&#39;0&#39;) like vBranch
                    <br />
                    AND C.F07 LIKE vSender<br />
                    AND lpad(C.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch<br />
                    --and C.tad like vCitadNew<br />
                    AND C.F21 LIKE vCitad<br />
                    AND ((nvl(C.Product_Type,&#39;%&#39;) LIKE &#39;OL3&#39;) OR
                    <br />
                    (nvl(C.Product_Type,&#39;%&#39;) NOT LIKE &#39;OL3&#39;<br />
                    AND (C.F07&lt;&gt;C.F21 OR (C.F07=C.F21 AND 
                    lpad(C.Source_Branch,5,&#39;0&#39;)&lt;&gt;lpad(C.Tad,5,&#39;0&#39;))))
                    <br />
                    OR (length(C.GW_TRANS_NUM)=6 AND substr(C.Gw_Trans_Num,1,1)=&#39;6&#39;))
                    <br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd
                    <br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BCNDM on BCNDM.SIBS_BANK_CODE =<br />
                    substr(A.SOURCE_BRANCH, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    <br />
                    elsif substr(to_char(pDate, &#39;DDMMYYYY&#39;), 3, 6) =<br />
                    substr(to_char(vOndate, &#39;DDMMYYYY&#39;),<br />
                    3,<br />
                    6) then<br />
                    delete from BM_IBPS02_TEMP;<br />
                    --Lay du lieu trong ngay<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    --decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,<br />
                    A.TellerID,<br />
                    substr(BSEND.SIBS_BANK_CODE,-3) || &#39;-&#39; || BSEND.Bank_Name AS SRCBRANCHNAME<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.TellerID,<br />
                    C.F21<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)
                    <br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    --AND C.PRE_TAD IS NOT NULL<br />
                    --AND lpad(C.PRE_TAD,5,&#39;0&#39;) like vBranch
                    <br />
                    AND C.F07 LIKE vSender<br />
                    AND lpad(C.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch<br />
                    --and C.tad like vCitadNew<br />
                    AND C.F21 LIKE vCitad<br />
                    AND ((nvl(C.Product_Type,&#39;%&#39;) LIKE &#39;OL3&#39;) OR
                    <br />
                    (nvl(C.Product_Type,&#39;%&#39;) NOT LIKE &#39;OL3&#39;<br />
                    AND (C.F07&lt;&gt;C.F21 OR (C.F07=C.F21 AND 
                    lpad(C.Source_Branch,5,&#39;0&#39;)&lt;&gt;lpad(C.Tad,5,&#39;0&#39;))))
                    <br />
                    OR (length(C.GW_TRANS_NUM)=6 AND substr(C.Gw_Trans_Num,1,1)=&#39;6&#39;))
                    <br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd
                    <br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BCNDM on BCNDM.SIBS_BANK_CODE =<br />
                    substr(A.SOURCE_BRANCH, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --Lay du lieu trong thang<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    --decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,<br />
                    A.TellerID,<br />
                    substr(BSEND.SIBS_BANK_CODE,-3) || &#39;-&#39; || BSEND.Bank_Name AS SRCBRANCHNAME<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.TellerID,<br />
                    C.F21<br />
                    FROM IBPS_MSG_ALL C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)
                    <br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    --AND C.PRE_TAD IS NOT NULL<br />
                    --AND lpad(C.PRE_TAD,5,&#39;0&#39;) like vBranch
                    <br />
                    AND C.F07 LIKE vSender<br />
                    AND lpad(C.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch<br />
                    --and C.tad like vCitadNew<br />
                    AND C.F21 LIKE vCitad<br />
                    AND ((nvl(C.Product_Type,&#39;%&#39;) LIKE &#39;OL3&#39;) OR
                    <br />
                    (nvl(C.Product_Type,&#39;%&#39;) NOT LIKE &#39;OL3&#39;<br />
                    AND (C.F07&lt;&gt;C.F21 OR (C.F07=C.F21 AND 
                    lpad(C.Source_Branch,5,&#39;0&#39;)&lt;&gt;lpad(C.Tad,5,&#39;0&#39;))))
                    <br />
                    OR (length(C.GW_TRANS_NUM)=6 AND substr(C.Gw_Trans_Num,1,1)=&#39;6&#39;))
                    <br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd
                    <br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BCNDM on BCNDM.SIBS_BANK_CODE =<br />
                    substr(A.SOURCE_BRANCH, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --order by tad,RM_NUMBER asc;<br />
                    else<br />
                    delete from BM_IBPS02_TEMP;<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    --decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,<br />
                    A.Tellerid,<br />
                    substr(BSEND.SIBS_BANK_CODE,-3) || &#39;-&#39; || BSEND.Bank_Name AS SRCBRANCHNAME<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid,<br />
                    C.F21<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)
                    <br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    --AND C.PRE_TAD IS NOT NULL<br />
                    --AND lpad(C.PRE_TAD,5,&#39;0&#39;) like vBranch
                    <br />
                    AND C.F07 LIKE vSender<br />
                    AND lpad(C.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch<br />
                    --and C.tad like vCitadNew<br />
                    AND C.F21 LIKE vCitad<br />
                    AND ((nvl(C.Product_Type,&#39;%&#39;) LIKE &#39;OL3&#39;) OR
                    <br />
                    (nvl(C.Product_Type,&#39;%&#39;) NOT LIKE &#39;OL3&#39;<br />
                    AND (C.F07&lt;&gt;C.F21 OR (C.F07=C.F21 AND 
                    lpad(C.Source_Branch,5,&#39;0&#39;)&lt;&gt;lpad(C.Tad,5,&#39;0&#39;))))
                    <br />
                    OR (length(C.GW_TRANS_NUM)=6 AND substr(C.Gw_Trans_Num,1,1)=&#39;6&#39;))
                    <br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd
                    <br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BCNDM on BCNDM.SIBS_BANK_CODE =<br />
                    substr(A.SOURCE_BRANCH, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --order by tad,RM_NUMBER asc;<br />
                    --Lay du lieu trong thang<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    --decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,<br />
                    A.Tellerid,<br />
                    substr(BSEND.SIBS_BANK_CODE,-3) || &#39;-&#39; || BSEND.Bank_Name AS SRCBRANCHNAME<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid,<br />
                    C.F21<br />
                    FROM IBPS_MSG_ALL C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)
                    <br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    --AND C.PRE_TAD IS NOT NULL<br />
                    --AND lpad(C.PRE_TAD,5,&#39;0&#39;) like vBranch
                    <br />
                    AND C.F07 LIKE vSender<br />
                    AND lpad(C.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch<br />
                    --and C.tad like vCitadNew<br />
                    AND C.F21 LIKE vCitad<br />
                    AND ((nvl(C.Product_Type,&#39;%&#39;) LIKE &#39;OL3&#39;) OR
                    <br />
                    (nvl(C.Product_Type,&#39;%&#39;) NOT LIKE &#39;OL3&#39;<br />
                    AND (C.F07&lt;&gt;C.F21 OR (C.F07=C.F21 AND 
                    lpad(C.Source_Branch,5,&#39;0&#39;)&lt;&gt;lpad(C.Tad,5,&#39;0&#39;))))
                    <br />
                    OR (length(C.GW_TRANS_NUM)=6 AND substr(C.Gw_Trans_Num,1,1)=&#39;6&#39;))
                    <br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd
                    <br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BCNDM on BCNDM.SIBS_BANK_CODE =<br />
                    substr(A.SOURCE_BRANCH, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --Lay du lieu trong nam<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    --decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,<br />
                    A.Tellerid,<br />
                    substr(BSEND.SIBS_BANK_CODE,-3) || &#39;-&#39; || BSEND.Bank_Name AS SRCBRANCHNAME<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid,<br />
                    C.F21<br />
                    FROM IBPS_MSG_ALL_HIS C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)
                    <br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    --AND C.PRE_TAD IS NOT NULL<br />
                    --AND lpad(C.PRE_TAD,5,&#39;0&#39;) like vBranch
                    <br />
                    AND C.F07 LIKE vSender<br />
                    AND lpad(C.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch<br />
                    --and C.tad like vCitadNew<br />
                    AND C.F21 LIKE vCitad<br />
                    AND ((nvl(C.Product_Type,&#39;%&#39;) LIKE &#39;OL3&#39;) OR
                    <br />
                    (nvl(C.Product_Type,&#39;%&#39;) NOT LIKE &#39;OL3&#39;<br />
                    AND (C.F07&lt;&gt;C.F21 OR (C.F07=C.F21 AND 
                    lpad(C.Source_Branch,5,&#39;0&#39;)&lt;&gt;lpad(C.Tad,5,&#39;0&#39;))))
                    <br />
                    OR (length(C.GW_TRANS_NUM)=6 AND substr(C.Gw_Trans_Num,1,1)=&#39;6&#39;))
                    <br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd
                    <br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BCNDM on BCNDM.SIBS_BANK_CODE =<br />
                    substr(A.SOURCE_BRANCH, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    end if;<br />
                    <br />
                    -- get from backup
                    <br />
                    delete from BM_IBPS02_TEMP<br />
                    where query_id in (select query_id<br />
                    from ibps_msg_content_backup C<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(C.STATUS) = trim(B.STATUS)<br />
                    where to_char(TRANS_DATE, &#39;YYYYMMDD&#39;) =<br />
                    to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    and to_char(create_Date, &#39;YYYYMMDD&#39;) &lt;&gt;<br />
                    to_char(C.TRANS_DATE, &#39;YYYYMMDD&#39;)
                    <br />
                    -- Phuongbh: dien di ho co the trong ngay --&gt; phai lay trang thai cuoi
                    <br />
                    -- --&gt; ko delete nhung dien duoc backup vao ngay hom sau<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;
                    <br />
                    AND C.F07 LIKE vSender<br />
                    AND lpad(C.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch
                    <br />
                    AND C.F21 LIKE vCitad<br />
                    AND ((nvl(C.Product_Type,&#39;%&#39;) LIKE &#39;OL3&#39;) OR
                    <br />
                    (nvl(C.Product_Type,&#39;%&#39;) NOT LIKE &#39;OL3&#39;<br />
                    AND (C.F07&lt;&gt;C.F21 OR (C.F07=C.F21 AND 
                    lpad(C.Source_Branch,5,&#39;0&#39;)&lt;&gt;lpad(C.Tad,5,&#39;0&#39;))))
                    <br />
                    OR (length(C.GW_TRANS_NUM)=6 AND substr(C.Gw_Trans_Num,1,1)=&#39;6&#39;))
                    <br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd ) ;<br />
                    <br />
                    <br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    --decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,<br />
                    A.Tellerid,<br />
                    substr(BSEND.SIBS_BANK_CODE,-3) || &#39;-&#39; || BSEND.Bank_Name AS SRCBRANCHNAME<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid,<br />
                    C.F21<br />
                    FROM IBPS_MSG_Content_backup C<br />
                    WHERE
                    <br />
                    C.ID IN
                    <br />
                    (Select Max(C1.ID) from IBPS_MSG_Content_backup C1
                    <br />
                    where C1.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    and to_char(C1.create_Date,&#39;YYYYMMDD&#39;)&lt;&gt;to_char(C1.TRANS_DATE, &#39;YYYYMMDD&#39;)
                    <br />
                    AND trim(C1.MSG_DIRECTION) = &#39;SIBS-IBPS&#39;<br />
                    --AND C.PRE_TAD IS NOT NULL<br />
                    --AND lpad(C.PRE_TAD,5,&#39;0&#39;) like vBranch
                    <br />
                    AND C1.F07 LIKE vSender<br />
                    AND lpad(C1.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch<br />
                    --and C.tad like vCitadNew<br />
                    AND C1.F21 LIKE vCitad<br />
                    AND ((nvl(C1.Product_Type,&#39;%&#39;) LIKE &#39;OL3&#39;) OR
                    <br />
                    (nvl(C1.Product_Type,&#39;%&#39;) NOT LIKE &#39;OL3&#39;<br />
                    AND (C1.F07&lt;&gt;C.F21 OR (C1.F07=C.F21 AND 
                    lpad(C1.Source_Branch,5,&#39;0&#39;)&lt;&gt;lpad(C1.Tad,5,&#39;0&#39;))))
                    <br />
                    OR (length(C1.GW_TRANS_NUM)=6 AND substr(C1.Gw_Trans_Num,1,1)=&#39;6&#39;))
                    <br />
                    AND nvl(C1.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    group by C1.Query_Id )<br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BCNDM on BCNDM.SIBS_BANK_CODE =<br />
                    substr(A.SOURCE_BRANCH, 3, 3)<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE ) ;
                    <br />
                    <br />
                    <br />
                    open RefCurBM_IBPS07 for<br />
                    select * from BM_IBPS02_TEMP order by GW_TRANS_NUM, RM_NUMBER asc;<br />
                    <br />
                    END MSB_BM_IBPS_07;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;MSB_BM_IBPS_09&quot; (pDate IN DATE,<br />
                    pCITAD IN VARCHAR2,--citad ma 8 so<br />
                    pCCYCD IN VARCHAR2 Default &#39;VND&#39;,<br />
                    RefCurBM_IBPS09 IN OUT PKG_CR.RefCurType) IS<br />
                    <br />
                    vCitad VARCHAR2(10);<br />
                    vCcycd VARCHAR2(10);<br />
                    vBranchName varchar2(200);<br />
                    vOndate date;<br />
                    BEGIN<br />
                    select sysdate into vOndate from dual;<br />
                    if upper(trim(pCitad)) = &#39;ALL&#39; or trim(pCitad) is null then<br />
                    vCitad := &#39;%&#39;;<br />
                    vBranchName:=&#39;ALL&#39;;<br />
                    else<br />
                    vCitad := trim(pCitad);<br />
                    select substr(T.Sibs_Code,-3) || &#39;-&#39; || T.Tad_Name into vBranchName from tad T 
                    where T.Gw_Bank_Code=pCitad and rownum=1;<br />
                    end if;<br />
                    if upper(trim(pCcycd)) = &#39;ALL&#39; or trim(pCcycd) is null then<br />
                    vCcycd := &#39;%&#39;;<br />
                    else<br />
                    vCcycd := trim(pCcycd);<br />
                    end if;<br />
                    <br />
                    if to_char(pDate, &#39;DDMMYYYY&#39;)= to_char(vOndate, &#39;DDMMYYYY&#39;) then<br />
                    DELETE FROM BM_IBPS02_TEMP;<br />
                    --Du lieu trong ngay<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    TRAN_CODE,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,
                    <br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    vBranchName,<br />
                    A.Tellerid,<br />
                    BCNDM.SIBS_BANK_CODE || &#39;-&#39; || BCNDM.BRAN_NAME AS SRCNAME,<br />
                    SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS BUTTOAN,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;011&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 LIKE vCitad<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN (&#39;8&#39;)) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BCNDM on lpad(BCNDM.SIBS_BANK_CODE,5,&#39;0&#39;) =<br />
                    lpad(A.SOURCE_BRANCH, 5, &#39;0&#39;)<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    <br />
                    elsif substr(to_char(pDate, &#39;DDMMYYYY&#39;), 3, 6) =<br />
                    substr(to_char(vOndate, &#39;DDMMYYYY&#39;),<br />
                    3,<br />
                    6) then<br />
                    delete from BM_IBPS02_TEMP;<br />
                    --Lay du lieu trong ngay<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    vBranchName,<br />
                    A.TellerID,<br />
                    BCNDM.SIBS_BANK_CODE || &#39;-&#39; || BCNDM.BRAN_NAME AS SRCNAME,<br />
                    SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.TellerID<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 LIKE vCitad<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN (&#39;8&#39;)) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BCNDM on lpad(BCNDM.SIBS_BANK_CODE,5,&#39;0&#39;) =<br />
                    lpad(A.SOURCE_BRANCH, 5, &#39;0&#39;)<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --Lay du lieu trong thang<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    vBranchName,<br />
                    A.TellerID,<br />
                    BCNDM.SIBS_BANK_CODE || &#39;-&#39; || BCNDM.BRAN_NAME AS SRCNAME,<br />
                    SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.TellerID<br />
                    FROM IBPS_MSG_ALL C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 LIKE vCitad<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN (&#39;8&#39;)) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BCNDM on lpad(BCNDM.SIBS_BANK_CODE,5,&#39;0&#39;) =<br />
                    lpad(A.SOURCE_BRANCH, 5, &#39;0&#39;)<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --order by tad,RM_NUMBER asc;<br />
                    else<br />
                    delete from BM_IBPS02_TEMP;<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    vBranchName,<br />
                    A.Tellerid,<br />
                    BCNDM.SIBS_BANK_CODE || &#39;-&#39; || BCNDM.BRAN_NAME AS SRCNAME,<br />
                    SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 LIKE vCitad<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN (&#39;8&#39;)) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BCNDM on lpad(BCNDM.SIBS_BANK_CODE,5,&#39;0&#39;) =<br />
                    lpad(A.SOURCE_BRANCH, 5, &#39;0&#39;)<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --order by tad,RM_NUMBER asc;<br />
                    --Lay du lieu trong thang<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    vBranchName,<br />
                    A.Tellerid,<br />
                    BCNDM.SIBS_BANK_CODE || &#39;-&#39; || BCNDM.BRAN_NAME AS SRCNAME,<br />
                    SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid<br />
                    FROM IBPS_MSG_ALL C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 LIKE vCitad<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN (&#39;8&#39;)) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BCNDM on lpad(BCNDM.SIBS_BANK_CODE,5,&#39;0&#39;) =<br />
                    lpad(A.SOURCE_BRANCH, 5, &#39;0&#39;)<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --Lay du lieu trong nam<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    vBranchName,<br />
                    A.Tellerid,<br />
                    BCNDM.SIBS_BANK_CODE || &#39;-&#39; || BCNDM.BRAN_NAME AS SRCNAME,<br />
                    SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid<br />
                    FROM IBPS_MSG_ALL_HIS C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 LIKE vCitad<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN (&#39;8&#39;)) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BCNDM on lpad(BCNDM.SIBS_BANK_CODE,5,&#39;0&#39;) =<br />
                    lpad(A.SOURCE_BRANCH, 5, &#39;0&#39;)<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    end if;<br />
                    <br />
                    -- get from backup
                    <br />
                    delete from BM_IBPS02_TEMP<br />
                    where query_id in (select query_id<br />
                    from ibps_msg_content_backup C
                    <br />
                    where to_char(TRANS_DATE, &#39;YYYYMMDD&#39;) =<br />
                    to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    and to_char(create_Date, &#39;YYYYMMDD&#39;) &lt;&gt;<br />
                    to_char(C.TRANS_DATE, &#39;YYYYMMDD&#39;)
                    <br />
                    -- Phuongbh: dien di ho co the trong ngay --&gt; phai lay trang thai cuoi
                    <br />
                    -- --&gt; ko delete nhung dien duoc backup vao ngay hom sau<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 LIKE vCitad<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN (&#39;8&#39;) ) ;<br />
                    <br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    vBranchName,<br />
                    A.Tellerid,<br />
                    BCNDM.SIBS_BANK_CODE || &#39;-&#39; || BCNDM.BRAN_NAME AS SRCNAME,<br />
                    SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid<br />
                    FROM IBPS_MSG_Content_backup C<br />
                    WHERE
                    <br />
                    C.ID IN
                    <br />
                    (Select Max(C1.ID) from IBPS_MSG_Content_backup C1
                    <br />
                    where C1.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    and to_char(C1.create_Date,&#39;YYYYMMDD&#39;)&lt;&gt;to_char(C1.TRANS_DATE, &#39;YYYYMMDD&#39;)
                    <br />
                    <br />
                    AND nvl(C1.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C1.F21 LIKE vCitad<br />
                    AND LENGTH(TO_CHAR(C1.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C1.GW_TRANS_NUM), 1, 1) IN (&#39;8&#39;))<br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN BRANCH BCNDM on lpad(BCNDM.SIBS_BANK_CODE,5,&#39;0&#39;) =<br />
                    lpad(A.SOURCE_BRANCH, 5, &#39;0&#39;) ;
                    <br />
                    <br />
                    <br />
                    open RefCurBM_IBPS09 for<br />
                    select * from BM_IBPS02_TEMP order by GW_TRANS_NUM,RM_NUMBER asc;<br />
                    END MSB_BM_IBPS_09;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;MSB_BM_IBPS_09_CN&quot; (pDate IN DATE,<br />
                    pCITAD IN VARCHAR2, --sender f07 + tad<br />
                    pCCYCD IN VARCHAR2 Default &#39;VND&#39;,<br />
                    RefCurBM_IBPS09_CN IN OUT PKG_CR.RefCurType) IS<br />
                    <br />
                    vSender varchar2(50); --sender 8 so
                    <br />
                    --vCitad VARCHAR2(10);<br />
                    vCcycd VARCHAR2(10);<br />
                    vSrcBranch varchar2(50);<br />
                    vBranchName varchar2(150);<br />
                    vOndate date;<br />
                    BEGIN<br />
                    select sysdate into vOndate from dual;<br />
                    if upper(trim(pCitad)) = &#39;ALL&#39; or trim(pCitad) is null then<br />
                    vSender := &#39;%&#39;;<br />
                    vSrcBranch:=&#39;%&#39;;<br />
                    vBranchName:=&#39;ALL&#39;;<br />
                    else<br />
                    GW_PK_IBPS_REPORT.SPLIT_SENDER(trim(pCitad),&#39;-&#39;,vSrcBranch,vSender);<br />
                    vSrcBranch:= lpad(vSrcBranch,5,&#39;0&#39;);<br />
                    SELECT M.Bank_Name into vBranchName<br />
                    FROM Ibps_Bank_Map M where lpad(M.Sibs_Bank_Code,5,&#39;0&#39;)= vSrcBranch and
                    <br />
                    M.Gw_Bank_Code=vSender and rownum=1;<br />
                    <br />
                    end if;<br />
                    /*<br />
                    if upper(trim(pCitad)) = &#39;ALL&#39; or trim(pCitad) is null then<br />
                    vCitad := &#39;%&#39;;<br />
                    else<br />
                    vCitad := trim(pCitad);<br />
                    end if;*/<br />
                    if upper(trim(pCcycd)) = &#39;ALL&#39; or trim(pCcycd) is null then<br />
                    vCcycd := &#39;%&#39;;<br />
                    else<br />
                    vCcycd := trim(pCcycd);<br />
                    end if;<br />
                    <br />
                    if to_char(pDate, &#39;DDMMYYYY&#39;)= to_char(vOndate, &#39;DDMMYYYY&#39;) then<br />
                    DELETE FROM BM_IBPS02_TEMP;<br />
                    --Du lieu trong ngay<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    TRAN_CODE,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BrNAme,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,
                    <br />
                    A.MSG_DIRECTION,<br />
                    vBranchName,<br />
                    A.Tellerid,<br />
                    BCNDM.Gw_Bank_Code || &#39;-&#39; || BCNDM.Tad_Name AS TADNAME,<br />
                    SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS BUTTOAN,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;011&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND C.ccycd LIKE vCcycd<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN (&#39;8&#39;)<br />
                    --AND C.f07 like vCitad<br />
                    AND C.F07 LIKE vSender
                    <br />
                    AND lpad(C.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch<br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    --LEFT JOIN BRANCH BCNDM on BCNDM.SIBS_BANK_CODE =<br />
                    -- substr(A.TAD, 3, 3)<br />
                    LEFT JOIN TAD BCNDM ON lpad(BCNDM.Sibs_Code,5,&#39;0&#39;) =<br />
                    lpad(A.TAD, 5, &#39;0&#39;)<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    <br />
                    elsif substr(to_char(pDate, &#39;DDMMYYYY&#39;), 3, 6) =<br />
                    substr(to_char(vOndate, &#39;DDMMYYYY&#39;),<br />
                    3,<br />
                    6) then<br />
                    delete from BM_IBPS02_TEMP;<br />
                    --Lay du lieu trong ngay<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    vBranchName,<br />
                    A.TellerID,<br />
                    BCNDM.Gw_Bank_Code || &#39;-&#39; || BCNDM.Tad_Name AS TADNAME,<br />
                    SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.TellerID<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN (&#39;8&#39;)<br />
                    --AND C.f07 like vCitad<br />
                    AND C.F07 LIKE vSender
                    <br />
                    AND lpad(C.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch<br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    --LEFT JOIN BRANCH BCNDM on BCNDM.SIBS_BANK_CODE =<br />
                    -- substr(A.TAD, 3, 3)<br />
                    LEFT JOIN TAD BCNDM ON lpad(BCNDM.Sibs_Code,5,&#39;0&#39;) =<br />
                    lpad(A.TAD, 5, &#39;0&#39;)<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --Lay du lieu trong thang<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    vBranchName AS BRNAME,<br />
                    A.TellerID,<br />
                    BCNDM.Gw_Bank_Code || &#39;-&#39; || BCNDM.Tad_Name AS TADNAME,<br />
                    SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.TellerID<br />
                    FROM IBPS_MSG_ALL C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN (&#39;8&#39;)<br />
                    --AND C.f07 like vCitad<br />
                    AND C.F07 LIKE vSender
                    <br />
                    AND lpad(C.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch<br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    --LEFT JOIN BRANCH BCNDM on BCNDM.SIBS_BANK_CODE =<br />
                    -- substr(A.TAD, 3, 3)<br />
                    LEFT JOIN TAD BCNDM ON lpad(BCNDM.Sibs_Code,5,&#39;0&#39;) =<br />
                    lpad(A.TAD, 5, &#39;0&#39;)<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --order by tad,RM_NUMBER asc;<br />
                    else<br />
                    delete from BM_IBPS02_TEMP;<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    vBranchName AS BRNAME,<br />
                    A.Tellerid,<br />
                    BCNDM.Gw_Bank_Code || &#39;-&#39; || BCNDM.Tad_Name AS TADNAME,<br />
                    SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN (&#39;8&#39;)<br />
                    --AND C.f07 like vCitad<br />
                    AND C.F07 LIKE vSender
                    <br />
                    AND lpad(C.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch<br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    --LEFT JOIN BRANCH BCNDM on BCNDM.SIBS_BANK_CODE =<br />
                    -- substr(A.TAD, 3, 3)<br />
                    LEFT JOIN TAD BCNDM ON lpad(BCNDM.Sibs_Code,5,&#39;0&#39;) =<br />
                    lpad(A.TAD, 5, &#39;0&#39;)<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --order by tad,RM_NUMBER asc;<br />
                    --Lay du lieu trong thang<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    vBranchName AS BRNAME,<br />
                    A.Tellerid,<br />
                    BCNDM.Gw_Bank_Code || &#39;-&#39; || BCNDM.Tad_Name AS TADNAME,<br />
                    SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid<br />
                    FROM IBPS_MSG_ALL C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN (&#39;8&#39;)<br />
                    --AND C.f07 like vCitad<br />
                    AND C.F07 LIKE vSender
                    <br />
                    AND lpad(C.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch<br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    --LEFT JOIN BRANCH BCNDM on BCNDM.SIBS_BANK_CODE =<br />
                    -- substr(A.TAD, 3, 3)<br />
                    LEFT JOIN TAD BCNDM ON lpad(BCNDM.Sibs_Code,5,&#39;0&#39;) =<br />
                    lpad(A.TAD, 5, &#39;0&#39;)<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --Lay du lieu trong nam<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    vBranchName AS BRNAME,<br />
                    A.Tellerid,<br />
                    BCNDM.Gw_Bank_Code || &#39;-&#39; || BCNDM.Tad_Name AS TADNAME,<br />
                    SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid<br />
                    FROM IBPS_MSG_ALL_HIS C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN (&#39;8&#39;)<br />
                    --AND C.f07 like vCitad<br />
                    AND C.F07 LIKE vSender
                    <br />
                    AND lpad(C.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch<br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    --LEFT JOIN BRANCH BCNDM on BCNDM.SIBS_BANK_CODE =<br />
                    -- substr(A.TAD, 3, 3)<br />
                    LEFT JOIN TAD BCNDM ON lpad(BCNDM.Sibs_Code,5,&#39;0&#39;) =<br />
                    lpad(A.TAD, 5, &#39;0&#39;)<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    end if;<br />
                    <br />
                    <br />
                    -- get from backup
                    <br />
                    delete from BM_IBPS02_TEMP<br />
                    where query_id in (select query_id<br />
                    from ibps_msg_content_backup C
                    <br />
                    where to_char(TRANS_DATE, &#39;YYYYMMDD&#39;) =<br />
                    to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    and to_char(create_Date, &#39;YYYYMMDD&#39;) &lt;&gt;<br />
                    to_char(C.TRANS_DATE, &#39;YYYYMMDD&#39;)
                    <br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND LENGTH(TO_CHAR(C.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C.GW_TRANS_NUM), 1, 1) IN (&#39;8&#39;)<br />
                    --AND C.f07 like vCitad<br />
                    AND C.F07 LIKE vSender
                    <br />
                    AND lpad(C.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch ) ;<br />
                    <br />
                    <br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    vBranchName AS BRNAME,<br />
                    A.Tellerid,<br />
                    BCNDM.Gw_Bank_Code || &#39;-&#39; || BCNDM.Tad_Name AS TADNAME,<br />
                    SUBSTR(TO_CHAR(A.GW_TRANS_NUM), 1, 2) AS RESEND_TYPE<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid<br />
                    FROM IBPS_MSG_CONTENT_BACKUP C<br />
                    WHERE
                    <br />
                    C.ID IN
                    <br />
                    (Select Max(C1.ID) from IBPS_MSG_Content_backup C1
                    <br />
                    where C1.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    and to_char(C1.create_Date,&#39;YYYYMMDD&#39;)&lt;&gt;to_char(C1.TRANS_DATE, &#39;YYYYMMDD&#39;)
                    <br />
                    <br />
                    AND nvl(C1.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND LENGTH(TO_CHAR(C1.GW_TRANS_NUM)) = 6<br />
                    AND SUBSTR(TO_CHAR(C1.GW_TRANS_NUM), 1, 1) IN (&#39;8&#39;)<br />
                    --AND C.f07 like vCitad<br />
                    AND C1.F07 LIKE vSender
                    <br />
                    AND lpad(C1.Source_Branch,5,&#39;0&#39;) LIKE vSrcBranch)<br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    --LEFT JOIN BRANCH BCNDM on BCNDM.SIBS_BANK_CODE =<br />
                    -- substr(A.TAD, 3, 3)<br />
                    LEFT JOIN TAD BCNDM ON lpad(BCNDM.Sibs_Code,5,&#39;0&#39;) =<br />
                    lpad(A.TAD, 5, &#39;0&#39;);
                    <br />
                    <br />
                    <br />
                    open RefCurBM_IBPS09_CN for<br />
                    select * from BM_IBPS02_TEMP order by GW_TRANS_NUM,RM_NUMBER asc;<br />
                    END MSB_BM_IBPS_09_CN;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;MSB_BM_IBPS_10&quot; (pdate IN date,<br />
                    pCcycd in varchar2 default &#39;VND&#39;,<br />
                    pCitad in varchar2,<br />
                    RefCurBM_IBPS10 IN OUT PKG_CR.RefCurType) IS<br />
                    vCitad varchar2(50);<br />
                    vCcycd varchar2(3);<br />
                    vBranchName varchar2(200);<br />
                    BEGIN<br />
                    if upper(trim(pCitad)) = &#39;ALL&#39; or trim(pCitad) is null then<br />
                    vCitad := &#39;%&#39;;<br />
                    vBranchName := &#39;ALL&#39;;<br />
                    else<br />
                    -- lay ma gia 3 so cong tad de so sanh dieu kien
                    <br />
                    select lpad(T.Sibs_Code, 5, &#39;0&#39;)<br />
                    into vCitad<br />
                    from tad T<br />
                    where T.Gw_Bank_Code = pCitad<br />
                    and rownum = 1;<br />
                    --vCitad := trim(pCitad);<br />
                    select substr(T.Sibs_Code, -3) || &#39; - &#39; || T.TAD_NAME<br />
                    into vBranchName<br />
                    from TAD T<br />
                    WHERE T.GW_BANK_CODE = pCitad<br />
                    and rownum = 1;<br />
                    end if;<br />
                    if upper(trim(pCcycd)) = &#39;ALL&#39; or trim(pCcycd) is null then<br />
                    vCcycd := &#39;%&#39;;<br />
                    else<br />
                    vCcycd := trim(pCcycd);<br />
                    end if;<br />
                    <br />
                    open RefCurBM_IBPS10 for<br />
                    SELECT A.msg_id,<br />
                    A.rm_number,<br />
                    A.sender,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    A.receiver,<br />
                    BRECV.BANK_NAME AS RECEINAME,<br />
                    A.amount,<br />
                    A.ccy,<br />
                    A.trans_date,<br />
                    A.exception_type,<br />
                    A.msg_direction,<br />
                    B.Content AS Status,<br />
                    --decode(C.Query_Id, NULL,decode(CA.Query_Id,NULL, CH.F21,CA.F21),C.F21 ) AS 
                    TAD,<br />
                    decode(trim(A.Tad), NULL, &#39; &#39;, to_char(to_number(A.tad))) AS TAD,<br />
                    --substr(A.tad, -3) AS TAD,<br />
                    T.GW_BANK_CODE || &#39; - &#39; || T.TAD_NAME AS TADNAME,<br />
                    GW_PK_IBPS_REPORT.IBPS_GET_Field(decode(C.Query_Id,<br />
                    NULL,<br />
                    decode(CA.Query_Id,<br />
                    NULL,<br />
                    CH.Content,<br />
                    CA.Content),<br />
                    C.Content),<br />
                    &#39;011&#39;) AS SOGD,<br />
                    --decode(C.Query_Id, NULL,decode(CA.Query_Id,NULL, 
                    CH.Gw_Trans_Num,CA.Gw_Trans_Num),C.GW_TRANS_NUM) AS GW_TRANS_NUM,<br />
                    to_number(substr(A.K1, -8)) AS GW_TRANS_NUM,<br />
                    decode(CB.Query_Id,<br />
                    NULL,<br />
                    decode(C.Query_Id,<br />
                    NULL,<br />
                    decode(CA.Query_Id,<br />
                    NULL,<br />
                    CH.Trans_Code,<br />
                    CA.Trans_Code),<br />
                    C.Trans_Code),<br />
                    CB.Trans_code) AS TRANS_CODE,<br />
                    decode(CB.Query_Id,<br />
                    NULL,<br />
                    decode(C.Query_Id,<br />
                    NULL,<br />
                    decode(CA.Query_Id,<br />
                    NULL,<br />
                    CH.SOURCE_BRANCH,<br />
                    CA.SOURCE_BRANCH),<br />
                    C.SOURCE_BRANCH),<br />
                    CB.SOURCE_BRANCH) AS SOURCE_BRANCH,<br />
                    vBranchName AS SRC_BRANCHNAME,<br />
                    decode(CB.Query_Id,<br />
                    NULL,<br />
                    decode(C.Query_Id,<br />
                    NULL,<br />
                    decode(CA.Query_Id, NULL, CH.MSG_SRC, CA.MSG_SRC),<br />
                    C.MSG_SRC),<br />
                    CB.MSG_SRC) AS MSG_SRC<br />
                    /*,
                    <br />
                    decode(CB.Query_Id,<br />
                    NULL,<br />
                    decode(C.Query_Id,<br />
                    NULL,<br />
                    decode(CA.Query_Id,<br />
                    NULL,<br />
                    &#39;CH&#39;,<br />
                    &#39;CA&#39;),<br />
                    &#39;C&#39;),<br />
                    &#39;CB&#39;) AS TRANS_CODE*/
                    <br />
                    FROM IBPS_MSG_REC A<br />
                    LEFT JOIN IBPS_MSG_CONTENT C<br />
                    ON A.Query_Id = C.Query_Id<br />
                    LEFT JOIN IBPS_MSG_ALL CA<br />
                    ON CA.Query_Id = A.Query_Id<br />
                    LEFT JOIN IBPS_MSG_ALL_HIS CH<br />
                    ON A.Query_Id = CH.Query_Id<br />
                    LEFT JOIN (SELECT *<br />
                    FROM IBPS_MSG_CONTENT_BACKUP BK<br />
                    WHERE BK.ID IN<br />
                    (Select Max(C1.ID)<br />
                    from IBPS_MSG_Content_backup C1<br />
                    where C1.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    and to_char(C1.create_Date, &#39;YYYYMMDD&#39;) &lt;&gt;<br />
                    to_char(C1.TRANS_DATE, &#39;YYYYMMDD&#39;)<br />
                    group by C1.Query_Id)) CB<br />
                    ON CB.Query_Id = A.Query_Id<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(A.STATUS) = trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND<br />
                    on BSEND.gw_bank_code = A.SENDER<br />
                    AND BSEND.Sibs_Bank_Code =<br />
                    decode(BSEND.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(C.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV<br />
                    on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND BRECV.Sibs_Bank_Code =<br />
                    decode(BRECV.Sibs_Bank_Code,<br />
                    -1,<br />
                    -1,<br />
                    lpad(C.Source_Branch, 5, &#39;0&#39;))<br />
                    LEFT JOIN TAD T<br />
                    ON lpad(A.TAD, 5, &#39;0&#39;) = lpad(T.Sibs_Code, 5, &#39;0&#39;)<br />
                    where A.Ndate = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    --to_char(A.Trans_Date,&#39;YYYYMMDD&#39;)=to_char(pDate,&#39;YYYYMMDD&#39;)<br />
                    AND decode(trim(A.tad), NULL, &#39;%&#39;, lpad(A.Tad, 5, &#39;0&#39;)) like vCitad<br />
                    --AND decode(C.Query_Id, NULL,decode(CA.Query_Id,NULL, 
                    decode(CH.F21,NULL,&#39;%&#39;,CH.F21),CA.F21),C.F21 ) LIKE &#39;%&#39;<br />
                    AND nvl(A.CCY, &#39;%&#39;) LIKE vCcycd<br />
                    ORDER BY GW_TRANS_NUM;<br />
                    /*LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )*/<br />
                    <br />
                    END MSB_BM_IBPS_10;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;MSB_BM_IBPS03_2&quot; (pDate IN DATE,<br />
                    pCITAD IN VARCHAR2, -- citad ma 8 so<br />
                    pCCYCD IN VARCHAR2 Default &#39;VND&#39;,<br />
                    pStatus in varchar2,<br />
                    RefCurBM_IBPS03_2 IN OUT PKG_CR.RefCurType) IS<br />
                    <br />
                    vCitad VARCHAR2(10);<br />
                    vCcycd VARCHAR2(10);<br />
                    vStatus VARCHAR2(50);<br />
                    vOndate date;<br />
                    vBranchName varchar2(200);<br />
                    BEGIN<br />
                    select sysdate into vOndate from dual;<br />
                    if upper(trim(pCitad)) = &#39;ALL&#39; or trim(pCitad) is null then<br />
                    vCitad := &#39;%&#39;;<br />
                    vBranchName:=&#39;ALL&#39;;<br />
                    else<br />
                    vCitad := trim(pCitad);<br />
                    select substr(T.Sibs_Code,-3) || &#39;-&#39; || T.Tad_Name into vBranchName from tad T 
                    where T.Gw_Bank_Code=pCitad and rownum=1;<br />
                    end if;<br />
                    if upper(trim(pCcycd)) = &#39;ALL&#39; or trim(pCcycd) is null then<br />
                    vCcycd := &#39;%&#39;;<br />
                    else<br />
                    vCcycd := trim(pCcycd);<br />
                    end if;<br />
                    if upper(trim(pStatus)) = &#39;ALL&#39; or trim(pStatus) is null then<br />
                    vStatus := &#39;%&#39;;<br />
                    else<br />
                    vStatus := trim(pStatus);<br />
                    end if;<br />
                    <br />
                    if to_char(pDate, &#39;DDMMYYYY&#39;)= to_char(vOndate, &#39;DDMMYYYY&#39;) then<br />
                    DELETE FROM BM_IBPS02_TEMP;<br />
                    --Du lieu trong ngay<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    TRAN_CODE,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02, -- tad name<br />
                    EXTFIELD03, -- ten nguoi huong<br />
                    EXTFIELD04) -- Msg-src<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    --decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,
                    <br />
                    A.Tellerid,<br />
                    substr(BCNDM.SIBS_BANK_CODE,-3) || &#39; - &#39; || BCNDM.BANK_NAME AS BRAN_NAME,<br />
                    A.NGUOIHUONG,<br />
                    A.Msg_Src<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS BUTTOAN,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;011&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;031&#39;) AS NGUOIHUONG,<br />
                    C.Msg_Src,<br />
                    C.F21<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 like vCitad<br />
                    AND C.Msg_Src IN (&#39;2&#39;, &#39;3&#39;)) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    <br />
                    LEFT JOIN IBPS_BANK_MAP BCNDM on BCNDM.GW_BANK_CODE = A.SENDER<br />
                    AND 
                    BCNDM.Sibs_Bank_Code=decode(BCNDM.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    where B.content like vStatus<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    <br />
                    elsif substr(to_char(pDate, &#39;DDMMYYYY&#39;), 3, 6) =<br />
                    substr(to_char(vOndate, &#39;DDMMYYYY&#39;),<br />
                    3,<br />
                    6) then<br />
                    delete from BM_IBPS02_TEMP;<br />
                    --Lay du lieu trong ngay<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03, -- ten nguoi huong<br />
                    EXTFIELD04) -- Msg-src<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    --decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,
                    <br />
                    A.TellerID,<br />
                    substr(BCNDM.SIBS_BANK_CODE,-3) || &#39; - &#39; || BCNDM.BANK_NAME AS BRAN_NAME,<br />
                    A.NGUOIHUONG,<br />
                    A.Msg_Src<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.TellerID,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;031&#39;) AS NGUOIHUONG,<br />
                    C.Msg_Src,<br />
                    C.F21<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 like vCitad<br />
                    AND C.Msg_Src IN (&#39;2&#39;, &#39;3&#39;)) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    <br />
                    LEFT JOIN IBPS_BANK_MAP BCNDM on BCNDM.GW_BANK_CODE = A.SENDER<br />
                    AND 
                    BCNDM.Sibs_Bank_Code=decode(BCNDM.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    where B.content like vStatus<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --Lay du lieu trong thang<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03, -- ten nguoi huong<br />
                    EXTFIELD04) -- Msg-src<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    --decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,
                    <br />
                    A.TellerID,<br />
                    substr(BCNDM.SIBS_BANK_CODE,-3) || &#39; - &#39; || BCNDM.BANK_NAME AS BRAN_NAME,<br />
                    A.NGUOIHUONG,<br />
                    A.Msg_Src<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.TellerID,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;031&#39;) AS NGUOIHUONG,<br />
                    C.Msg_Src,<br />
                    C.F21<br />
                    FROM IBPS_MSG_ALL C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 like vCitad<br />
                    AND C.Msg_Src IN (&#39;2&#39;, &#39;3&#39;)) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    <br />
                    LEFT JOIN IBPS_BANK_MAP BCNDM on BCNDM.GW_BANK_CODE = A.SENDER<br />
                    AND 
                    BCNDM.Sibs_Bank_Code=decode(BCNDM.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    where B.content like vStatus<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --order by tad,RM_NUMBER asc;<br />
                    else<br />
                    delete from BM_IBPS02_TEMP;<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03, -- ten nguoi huong<br />
                    EXTFIELD04) -- Msg-src<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    --decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,
                    <br />
                    A.Tellerid,<br />
                    substr(BCNDM.SIBS_BANK_CODE,-3) || &#39; - &#39; || BCNDM.BANK_NAME AS BRAN_NAME,<br />
                    A.NGUOIHUONG,<br />
                    A.Msg_Src<br />
                    <br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;031&#39;) AS NGUOIHUONG,<br />
                    C.Msg_Src,<br />
                    C.F21<br />
                    FROM IBPS_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 like vCitad<br />
                    AND C.Msg_Src IN (&#39;2&#39;, &#39;3&#39;)) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BCNDM on BCNDM.GW_BANK_CODE = A.SENDER<br />
                    AND 
                    BCNDM.Sibs_Bank_Code=decode(BCNDM.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    where B.content like vStatus<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    --order by tad,RM_NUMBER asc;<br />
                    --Lay du lieu trong thang<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03, -- ten nguoi huong<br />
                    EXTFIELD04) -- Msg-src<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    --decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,
                    <br />
                    A.Tellerid,<br />
                    substr(BCNDM.SIBS_BANK_CODE,-3) || &#39; - &#39; || BCNDM.BANK_NAME AS BRAN_NAME,<br />
                    A.NGUOIHUONG,<br />
                    A.Msg_Src<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;031&#39;) AS NGUOIHUONG,<br />
                    C.Msg_Src,<br />
                    C.F21<br />
                    FROM IBPS_MSG_ALL C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 like vCitad<br />
                    AND C.Msg_Src IN (&#39;2&#39;, &#39;3&#39;)) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BCNDM on BCNDM.GW_BANK_CODE = A.SENDER<br />
                    AND 
                    BCNDM.Sibs_Bank_Code=decode(BCNDM.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    where B.content like vStatus<br />
                    ;<br />
                    --Lay du lieu trong nam<br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03, -- ten nguoi huong<br />
                    EXTFIELD04) -- Msg-src<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    --decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,
                    <br />
                    A.Tellerid,<br />
                    substr(BCNDM.SIBS_BANK_CODE,-3) || &#39; - &#39; || BCNDM.BANK_NAME AS BRAN_NAME,<br />
                    A.NGUOIHUONG,<br />
                    A.Msg_Src<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;031&#39;) AS NGUOIHUONG,<br />
                    C.Msg_Src,<br />
                    C.F21<br />
                    FROM IBPS_MSG_ALL_HIS C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 like vCitad<br />
                    AND C.Msg_Src IN (&#39;2&#39;, &#39;3&#39;)) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BCNDM on BCNDM.GW_BANK_CODE = A.SENDER<br />
                    AND 
                    BCNDM.Sibs_Bank_Code=decode(BCNDM.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    where B.content like vStatus<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    end if;<br />
                    <br />
                    -- get from backup<br />
                    delete from BM_IBPS02_TEMP<br />
                    where query_id in (select query_id<br />
                    from ibps_msg_content_backup C<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(C.STATUS) = trim(B.STATUS)<br />
                    where to_char(TRANS_DATE, &#39;YYYYMMDD&#39;) =<br />
                    to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    and to_char(create_Date, &#39;YYYYMMDD&#39;) &lt;&gt;<br />
                    to_char(C.TRANS_DATE, &#39;YYYYMMDD&#39;)<br />
                    AND nvl(C.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C.F21 like vCitad<br />
                    AND C.Msg_Src IN (&#39;2&#39;, &#39;3&#39;));<br />
                    <br />
                    insert into BM_IBPS02_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    GW_TRANS_NUM,<br />
                    SOGD,<br />
                    RM_NUMBER,<br />
                    SENDER,<br />
                    RECEIVER,<br />
                    AMOUNT,<br />
                    TRANS_DATE,<br />
                    SOURCE_BRANCH,<br />
                    TAD,<br />
                    PRE_TAD,<br />
                    STATUS,<br />
                    CCYCD,<br />
                    Tran_Code,<br />
                    Sendername,<br />
                    Receivername,<br />
                    Msg_Direction,<br />
                    BRNAME,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03, -- ten nguoi huong<br />
                    EXTFIELD04) -- Msg-src<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.GW_TRANS_NUM,<br />
                    A.SOGD,<br />
                    A.RM_NUMBER,<br />
                    A.SENDER,<br />
                    A.RECEIVER,<br />
                    A.AMOUNT,<br />
                    A.TRANS_DATE,<br />
                    A.SOURCE_BRANCH,<br />
                    A.TAD,<br />
                    A.PRE_TAD,<br />
                    B.CONTENT AS STATUS,<br />
                    A.CCYCD,<br />
                    A.Trans_Code,<br />
                    BSEND.BANK_NAME AS SENDERNAME,<br />
                    BRECV.Bank_Name AS RECEIVERNAME,<br />
                    A.MSG_DIRECTION,<br />
                    --decode(vCitad,&#39;%&#39;,&#39;ALL&#39;, T.GW_BANK_CODE || &#39; - &#39; || T.BANK_NAME),<br />
                    vBranchName,
                    <br />
                    A.Tellerid,<br />
                    substr(BCNDM.SIBS_BANK_CODE,-3) || &#39; - &#39; || BCNDM.BANK_NAME AS BRAN_NAME,<br />
                    A.NGUOIHUONG,<br />
                    A.Msg_Src<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    C.GW_TRANS_NUM,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;110&#39;) AS SOGD,<br />
                    to_number(C.RM_NUMBER) AS RM_NUMBER,<br />
                    C.F07 as SENDER,<br />
                    C.F22 AS RECEIVER,<br />
                    C.AMOUNT,<br />
                    C.TRANS_DATE,<br />
                    C.SOURCE_BRANCH,<br />
                    C.TAD,<br />
                    C.PRE_TAD,<br />
                    C.STATUS,<br />
                    C.CCYCD,<br />
                    C.Trans_Code,<br />
                    C.MSG_DIRECTION,<br />
                    C.Tellerid,<br />
                    GW_PK_LIB.GET_IBPS_Field(C.content, &#39;031&#39;) AS NGUOIHUONG,<br />
                    C.Msg_Src,<br />
                    C.F21<br />
                    FROM IBPS_MSG_CONTENT_BACKUP C<br />
                    WHERE
                    <br />
                    C.Id IN
                    <br />
                    (Select Max(C1.ID) from IBPS_MSG_Content_backup C1
                    <br />
                    where C1.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    and to_char(C1.create_Date,&#39;YYYYMMDD&#39;)&lt;&gt;to_char(C1.TRANS_DATE, &#39;YYYYMMDD&#39;)<br />
                    <br />
                    AND nvl(C1.ccycd,&#39;%&#39;) LIKE vCcycd<br />
                    AND C1.F21 like vCitad<br />
                    AND C1.Msg_Src IN (&#39;2&#39;, &#39;3&#39;))<br />
                    ) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    LEFT JOIN IBPS_BANK_MAP BSEND on BSEND.gw_bank_code = A.SENDER<br />
                    AND 
                    BSEND.Sibs_Bank_Code=decode(BSEND.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BRECV on BRECV.gw_bank_code = A.RECEIVER<br />
                    AND 
                    BRECV.Sibs_Bank_Code=decode(BRECV.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP BCNDM on BCNDM.GW_BANK_CODE = A.SENDER<br />
                    AND 
                    BCNDM.Sibs_Bank_Code=decode(BCNDM.Sibs_Bank_Code,-1,-1,lpad(A.Source_Branch,5,&#39;0&#39;))<br />
                    LEFT JOIN IBPS_BANK_MAP T ON T.GW_BANK_CODE =A.F21
                    <br />
                    and T.GW_BANK_CODE NOT in<br />
                    (select TAD from TADMAP where NOTE = T.SIBS_BANK_CODE )
                    <br />
                    where B.content like vStatus<br />
                    order by A.Trans_Code, RECEIVER asc;<br />
                    open RefCurBM_IBPS03_2 for<br />
                    select * from BM_IBPS02_TEMP order by GW_TRANS_NUM, RM_NUMBER asc;<br />
                    END MSB_BM_IBPS03_2;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;PROC3&quot; (RMnumber Varchar2,<br />
                    amount number,<br />
                    msgdate varchar2,<br />
                    content varchar2) is<br />
                    pragma autonomous_transaction;<br />
                    begin<br />
                    <br />
                    insert into reconcile_huy<br />
                    (Transdate,rm_number, amount, msg_date, content)<br />
                    values<br />
                    (sysdate,RMnumber, amount, msgdate, content);<br />
                    commit;<br />
                    <br />
                    end proc3;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;SW_EXP_EXCEL&quot; (vQUERYID varchar2,vReturn in out 
                    PKG_CR.RefCurType) is<br />
                    sql_stmt varchar2(4000);<br />
                    begin<br />
                    OPEN vReturn FOR<br />
                    &#39;SELECT 
                    msg_type,decode(msg_direction,&#39;&#39;SWIFT-SIBS&#39;&#39;,branch_b,&#39;&#39;SWIFT-SIBS&#39;&#39;,branch_a) 
                    branch,<br />
                    value_date,ccycd,amount,
                    <br />
                    get_field(CONTENT,&#39;&#39;20&#39;&#39;,1,0,0) as F20,<br />
                    get_field(CONTENT,&#39;&#39;21&#39;&#39;,1,0,0) as F21,<br />
                    get_field(CONTENT,&#39;&#39;33&#39;&#39;,1,0,1) as F33,<br />
                    get_field(CONTENT,&#39;&#39;33&#39;&#39;,1,0,2) as F33_1,<br />
                    get_field(CONTENT,&#39;&#39;33&#39;&#39;,1,0,3) as F33_2,<br />
                    get_field(CONTENT,&#39;&#39;50&#39;&#39;,1,1,0) as F50_1,<br />
                    get_field(CONTENT,&#39;&#39;50&#39;&#39;,2,0,0) as F50_2,<br />
                    get_field(CONTENT,&#39;&#39;52&#39;&#39;,1,1,0) as F52_1,<br />
                    get_field(CONTENT,&#39;&#39;52&#39;&#39;,2,0,0) as F52_2,<br />
                    get_field(CONTENT,&#39;&#39;53&#39;&#39;,1,1,0) as F53_1,<br />
                    get_field(CONTENT,&#39;&#39;53&#39;&#39;,2,0,0) as F53_2,<br />
                    get_field(CONTENT,&#39;&#39;54&#39;&#39;,1,1,0) as F54_1,<br />
                    get_field(CONTENT,&#39;&#39;54&#39;&#39;,2,0,0) as F54_2,<br />
                    get_field(CONTENT,&#39;&#39;55&#39;&#39;,1,1,0) as F55_1,<br />
                    get_field(CONTENT,&#39;&#39;55&#39;&#39;,2,0,0) as F55_2,<br />
                    get_field(CONTENT,&#39;&#39;56&#39;&#39;,1,1,0) as F56_1,<br />
                    get_field(CONTENT,&#39;&#39;56&#39;&#39;,2,0,0) as F56_2,<br />
                    get_field(CONTENT,&#39;&#39;57&#39;&#39;,1,1,0) as F57_1,<br />
                    get_field(CONTENT,&#39;&#39;57&#39;&#39;,2,0,0) as F57_2,<br />
                    get_field(CONTENT,&#39;&#39;58&#39;&#39;,1,1,0) as F58_1,<br />
                    get_field(CONTENT,&#39;&#39;58&#39;&#39;,2,0,0) as F58_2,<br />
                    get_field(CONTENT,&#39;&#39;59&#39;&#39;,1,1,0) as F59_1,<br />
                    get_field(CONTENT,&#39;&#39;59&#39;&#39;,2,0,0) as F59_2,<br />
                    get_field(CONTENT,&#39;&#39;70&#39;&#39;,1,0,0) as F70,<br />
                    get_field(CONTENT,&#39;&#39;71&#39;&#39;,1,0,0) as F71,<br />
                    get_field(CONTENT,&#39;&#39;72&#39;&#39;,1,0,0) as F72 FROM SWIFT_MSG_CONTENT<br />
                    where department=&#39;&#39;RM&#39;&#39;<br />
                    and QUERY_ID IN (&#39;|| vQUERYID ||&#39;) union
                    <br />
                    SELECT 
                    msg_type,decode(msg_direction,&#39;&#39;SWIFT-SIBS&#39;&#39;,branch_b,&#39;&#39;SWIFT-SIBS&#39;&#39;,branch_a) 
                    branch,<br />
                    value_date,ccycd,amount,
                    <br />
                    get_field(CONTENT,&#39;&#39;20&#39;&#39;,1,0,0) as F20,<br />
                    get_field(CONTENT,&#39;&#39;21&#39;&#39;,1,0,0) as F21,<br />
                    get_field(CONTENT,&#39;&#39;33&#39;&#39;,1,0,1) as F33,<br />
                    get_field(CONTENT,&#39;&#39;33&#39;&#39;,1,0,2) as F33_1,<br />
                    get_field(CONTENT,&#39;&#39;33&#39;&#39;,1,0,3) as F33_2,<br />
                    get_field(CONTENT,&#39;&#39;50&#39;&#39;,1,1,0) as F50_1,<br />
                    get_field(CONTENT,&#39;&#39;50&#39;&#39;,2,0,0) as F50_2,<br />
                    get_field(CONTENT,&#39;&#39;52&#39;&#39;,1,1,0) as F52_1,<br />
                    get_field(CONTENT,&#39;&#39;52&#39;&#39;,2,0,0) as F52_2,<br />
                    get_field(CONTENT,&#39;&#39;53&#39;&#39;,1,1,0) as F53_1,<br />
                    get_field(CONTENT,&#39;&#39;53&#39;&#39;,2,0,0) as F53_2,<br />
                    get_field(CONTENT,&#39;&#39;54&#39;&#39;,1,1,0) as F54_1,<br />
                    get_field(CONTENT,&#39;&#39;54&#39;&#39;,2,0,0) as F54_2,<br />
                    get_field(CONTENT,&#39;&#39;55&#39;&#39;,1,1,0) as F55_1,<br />
                    get_field(CONTENT,&#39;&#39;55&#39;&#39;,2,0,0) as F55_2,<br />
                    get_field(CONTENT,&#39;&#39;56&#39;&#39;,1,1,0) as F56_1,<br />
                    get_field(CONTENT,&#39;&#39;56&#39;&#39;,2,0,0) as F56_2,<br />
                    get_field(CONTENT,&#39;&#39;57&#39;&#39;,1,1,0) as F57_1,<br />
                    get_field(CONTENT,&#39;&#39;57&#39;&#39;,2,0,0) as F57_2,<br />
                    get_field(CONTENT,&#39;&#39;58&#39;&#39;,1,1,0) as F58_1,<br />
                    get_field(CONTENT,&#39;&#39;58&#39;&#39;,2,0,0) as F58_2,<br />
                    get_field(CONTENT,&#39;&#39;59&#39;&#39;,1,1,0) as F59_1,<br />
                    get_field(CONTENT,&#39;&#39;59&#39;&#39;,2,0,0) as F59_2,<br />
                    get_field(CONTENT,&#39;&#39;70&#39;&#39;,1,0,0) as F70,<br />
                    get_field(CONTENT,&#39;&#39;71&#39;&#39;,1,0,0) as F71,<br />
                    get_field(CONTENT,&#39;&#39;72&#39;&#39;,1,0,0) as F72 FROM SWIFT_MSG_ALL<br />
                    where department=&#39;&#39;RM&#39;&#39;<br />
                    and QUERY_ID IN (&#39;|| vQUERYID ||&#39;) union
                    <br />
                    SELECT 
                    msg_type,decode(msg_direction,&#39;&#39;SWIFT-SIBS&#39;&#39;,branch_b,&#39;&#39;SWIFT-SIBS&#39;&#39;,branch_a) 
                    branch,<br />
                    value_date,ccycd,amount,
                    <br />
                    get_field(CONTENT,&#39;&#39;20&#39;&#39;,1,0,0) as F20,<br />
                    get_field(CONTENT,&#39;&#39;21&#39;&#39;,1,0,0) as F21,<br />
                    get_field(CONTENT,&#39;&#39;33&#39;&#39;,1,0,1) as F33,<br />
                    get_field(CONTENT,&#39;&#39;33&#39;&#39;,1,0,2) as F33_1,<br />
                    get_field(CONTENT,&#39;&#39;33&#39;&#39;,1,0,3) as F33_2,<br />
                    get_field(CONTENT,&#39;&#39;50&#39;&#39;,1,1,0) as F50_1,<br />
                    get_field(CONTENT,&#39;&#39;50&#39;&#39;,2,0,0) as F50_2,<br />
                    get_field(CONTENT,&#39;&#39;52&#39;&#39;,1,1,0) as F52_1,<br />
                    get_field(CONTENT,&#39;&#39;52&#39;&#39;,2,0,0) as F52_2,<br />
                    get_field(CONTENT,&#39;&#39;53&#39;&#39;,1,1,0) as F53_1,<br />
                    get_field(CONTENT,&#39;&#39;53&#39;&#39;,2,0,0) as F53_2,<br />
                    get_field(CONTENT,&#39;&#39;54&#39;&#39;,1,1,0) as F54_1,<br />
                    get_field(CONTENT,&#39;&#39;54&#39;&#39;,2,0,0) as F54_2,<br />
                    get_field(CONTENT,&#39;&#39;55&#39;&#39;,1,1,0) as F55_1,<br />
                    get_field(CONTENT,&#39;&#39;55&#39;&#39;,2,0,0) as F55_2,<br />
                    get_field(CONTENT,&#39;&#39;56&#39;&#39;,1,1,0) as F56_1,<br />
                    get_field(CONTENT,&#39;&#39;56&#39;&#39;,2,0,0) as F56_2,<br />
                    get_field(CONTENT,&#39;&#39;57&#39;&#39;,1,1,0) as F57_1,<br />
                    get_field(CONTENT,&#39;&#39;57&#39;&#39;,2,0,0) as F57_2,<br />
                    get_field(CONTENT,&#39;&#39;58&#39;&#39;,1,1,0) as F58_1,<br />
                    get_field(CONTENT,&#39;&#39;58&#39;&#39;,2,0,0) as F58_2,<br />
                    get_field(CONTENT,&#39;&#39;59&#39;&#39;,1,1,0) as F59_1,<br />
                    get_field(CONTENT,&#39;&#39;59&#39;&#39;,2,0,0) as F59_2,<br />
                    get_field(CONTENT,&#39;&#39;70&#39;&#39;,1,0,0) as F70,<br />
                    get_field(CONTENT,&#39;&#39;71&#39;&#39;,1,0,0) as F71,<br />
                    get_field(CONTENT,&#39;&#39;72&#39;&#39;,1,0,0) as F72 FROM SWIFT_MSG_ALL_HIS<br />
                    where department=&#39;&#39;RM&#39;&#39;<br />
                    and QUERY_ID IN (&#39;|| vQUERYID ||&#39;)&#39; ;
                    <br />
                    <br />
                    <br />
                    <br />
                    end SW_EXP_EXCEL;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;SWIFT_FEE_CAL&quot; (<br />
                    pFromDate in date,<br />
                    pToDate in date,<br />
                    pBranch in varchar,<br />
                    pFeeType in NUMBER,<br />
                    RefCurBK02 IN OUT PKG_CR.RefCurType) IS<br />
                    strSql varchar2(4000);
                    <br />
                    vFR_DATE NUMBER(10);<br />
                    vTO_DATE NUMBER(10);<br />
                    BEGIN<br />
                    --<br />
                    vFR_DATE := TO_NUMBER(TO_CHAR(pFromDate, &#39;YYYYMMDD&#39;));<br />
                    vTO_DATE := TO_NUMBER(TO_CHAR(pToDate, &#39;YYYYMMDD&#39;));
                    <br />
                    --XOA DU LIEU BANG TEMP<br />
                    DELETE FROM SWIFT_FEE_CAL_TEMP;<br />
                    --INSERT DU LIEU VAO BANG TEMP<br />
                    IF pBranch=&#39;ALL&#39; THEN
                    <br />
                    INSERT INTO SWIFT_FEE_CAL_TEMP<br />
                    (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH, MSG_TYPE)<br />
                    SELECT MSG_ID,AMOUNT,CCYCD,TRANS_DATE,CONTENT,BRANCH_A,MSG_TYPE<br />
                    FROM SWIFT_MSG_CONTENT<br />
                    WHERE TRANSDATE &gt;= vFR_DATE<br />
                    AND TRANSDATE &lt;= vTO_DATE<br />
                    AND STATUS = 1<br />
                    --AND CCYCD LIKE &#39;%&#39; || DECODE(pCCYCD,&#39;ALL&#39;,&#39;&#39;,pCCYCD)<br />
                    AND MSG_DIRECTION = &#39;SIBS-SWIFT&#39;;<br />
                    INSERT INTO SWIFT_FEE_CAL_TEMP<br />
                    (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH,MSG_TYPE)<br />
                    SELECT MSG_ID,AMOUNT,CCYCD,TRANS_DATE,CONTENT,BRANCH_A,MSG_TYPE<br />
                    FROM SWIFT_MSG_ALL<br />
                    WHERE TO_CHAR(TRANSDATE) &gt;= TO_CHAR(pFromDate, &#39;YYYYMMDD&#39;)<br />
                    AND TO_CHAR(TRANSDATE) &lt;= TO_CHAR(pToDate, &#39;YYYYMMDD&#39;)<br />
                    AND STATUS = 1<br />
                    --AND CCYCD LIKE &#39;%&#39; || DECODE(pCCYCD,&#39;ALL&#39;,&#39;&#39;,pCCYCD)<br />
                    AND MSG_DIRECTION = &#39;SIBS-SWIFT&#39;;<br />
                    INSERT INTO SWIFT_FEE_CAL_TEMP<br />
                    (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH,MSG_TYPE)<br />
                    SELECT MSG_ID,AMOUNT,CCYCD,TRANS_DATE,CONTENT,BRANCH_A,MSG_TYPE<br />
                    FROM SWIFT_MSG_ALL_HIS<br />
                    WHERE TO_CHAR(TRANSDATE) &gt;= TO_CHAR(pFromDate, &#39;YYYYMMDD&#39;)<br />
                    AND TO_CHAR(TRANSDATE) &lt;= TO_CHAR(pToDate, &#39;YYYYMMDD&#39;)<br />
                    AND STATUS = 1<br />
                    --AND CCYCD LIKE &#39;%&#39; || DECODE(pCCYCD,&#39;ALL&#39;,&#39;&#39;,pCCYCD)<br />
                    AND MSG_DIRECTION = &#39;SIBS-SWIFT&#39;;<br />
                    ELSE<br />
                    INSERT INTO SWIFT_FEE_CAL_TEMP<br />
                    (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH,MSG_TYPE)<br />
                    SELECT MSG_ID,AMOUNT,CCYCD,TRANS_DATE,CONTENT,BRANCH_A,MSG_TYPE<br />
                    FROM SWIFT_MSG_CONTENT<br />
                    WHERE TRANSDATE &gt;= vFR_DATE<br />
                    AND TRANSDATE &lt;= vTO_DATE<br />
                    AND BRANCH_A = LPAD(pBranch,5,&#39;0&#39;)<br />
                    AND STATUS = 1<br />
                    --AND CCYCD LIKE &#39;%&#39; || DECODE(pCCYCD,&#39;ALL&#39;,&#39;&#39;,pCCYCD)<br />
                    AND MSG_DIRECTION = &#39;SIBS-SWIFT&#39;;<br />
                    INSERT INTO SWIFT_FEE_CAL_TEMP<br />
                    (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH,MSG_TYPE)<br />
                    SELECT MSG_ID,AMOUNT,CCYCD,TRANS_DATE,CONTENT,BRANCH_A,MSG_TYPE<br />
                    FROM SWIFT_MSG_ALL<br />
                    WHERE TO_CHAR(TRANSDATE) &gt;= TO_CHAR(pFromDate, &#39;YYYYMMDD&#39;)<br />
                    AND TO_CHAR(TRANSDATE) &lt;= TO_CHAR(pToDate, &#39;YYYYMMDD&#39;)<br />
                    AND BRANCH_A = LPAD(pBranch,5,&#39;0&#39;)<br />
                    AND STATUS = 1<br />
                    --AND CCYCD LIKE &#39;%&#39; || DECODE(pCCYCD,&#39;ALL&#39;,&#39;&#39;,pCCYCD)<br />
                    AND MSG_DIRECTION = &#39;SIBS-SWIFT&#39;;<br />
                    INSERT INTO SWIFT_FEE_CAL_TEMP<br />
                    (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH,MSG_TYPE)<br />
                    SELECT MSG_ID,AMOUNT,CCYCD,TRANS_DATE,CONTENT,BRANCH_A,MSG_TYPE<br />
                    FROM SWIFT_MSG_ALL_HIS<br />
                    WHERE TO_CHAR(TRANSDATE) &gt;= TO_CHAR(pFromDate, &#39;YYYYMMDD&#39;)<br />
                    AND TO_CHAR(TRANSDATE) &lt;= TO_CHAR(pToDate, &#39;YYYYMMDD&#39;)<br />
                    AND BRANCH_A = LPAD(pBranch,5,&#39;0&#39;)<br />
                    AND STATUS = 1<br />
                    --AND CCYCD LIKE &#39;%&#39; || DECODE(pCCYCD,&#39;ALL&#39;,&#39;&#39;,pCCYCD)<br />
                    AND MSG_DIRECTION = &#39;SIBS-SWIFT&#39;;<br />
                    END IF;<br />
                    --TINH PHI VOI LOAI PHI TY LE<br />
                    IF pFeeType = 2 THEN<br />
                    strSql := &#39;SELECT SUM(T.AMOUNT)AS FEE,COUNT(T.MSG_ID) AS NUM,
                    <br />
                    K.BRAN_NAME, BRANCH, CCYCD, MSG_TYPE<br />
                    FROM<br />
                    (SELECT A.MSG_ID,(CASE WHEN B.RATE_FEE * NVL(A.AMOUNT,0)/100 &gt;= B.MAX_FEE
                    <br />
                    then B.MAX_FEE when B.RATE_FEE * NVL(A.AMOUNT,0)/100&lt;= B.MIN_FEE then
                    <br />
                    B.MIN_FEE else B.RATE_FEE * NVL(A.AMOUNT,0)/100 END)as AMOUNT,<br />
                    BRANCH,A.CCYCD, B.MSG_TYPE
                    <br />
                    FROM SWIFT_FEE_CAL_TEMP A INNER JOIN SWIFT_FEE B
                    <br />
                    ON A.MSG_TYPE = B.MSG_TYPE
                    <br />
                    )T INNER JOIN BRANCH K ON
                    <br />
                    LPAD(T.BRANCH,5,&#39;&#39;0&#39;&#39;) = LPAD(K.SIBS_BANK_CODE,5,&#39;&#39;0&#39;&#39;)<br />
                    GROUP BY K.BRAN_NAME, T.BRANCH, T.CCYCD, MSG_TYPE<br />
                    ORDER BY BRAN_NAME&#39;;<br />
                    --TINH PHI THEO LOAI PHI CO DINH<br />
                    ELSE<br />
                    strSql := &#39;SELECT SUM(T.AMOUNT)AS FEE,COUNT(T.MSG_ID) AS NUM,
                    <br />
                    K.BRAN_NAME, BRANCH, CCYCD, MSG_TYPE<br />
                    FROM<br />
                    (SELECT A.MSG_ID,B.FIXED_FEE as AMOUNT,<br />
                    BRANCH,B.CCYCD, B.MSG_TYPE
                    <br />
                    FROM SWIFT_FEE_CAL_TEMP A INNER JOIN SWIFT_FEE B
                    <br />
                    ON A.MSG_TYPE = B.MSG_TYPE
                    <br />
                    )T INNER JOIN BRANCH K ON
                    <br />
                    LPAD(T.BRANCH,5,&#39;&#39;0&#39;&#39;) = LPAD(K.SIBS_BANK_CODE,5,&#39;&#39;0&#39;&#39;)<br />
                    GROUP BY K.BRAN_NAME, T.BRANCH, T.CCYCD, MSG_TYPE<br />
                    ORDER BY BRAN_NAME&#39;;<br />
                    END IF;<br />
                    <br />
                    OPEN RefCurBK02 FOR strSql;<br />
                    <br />
                    END SWIFT_FEE_CAL;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;SWIFT_PRINT_MSG_LIST&quot; (pMSG_ID IN NUMBER,<br />
                    pBranch IN varchar2,<br />
                    pUser IN varchar2,<br />
                    pcurBM_SWIFT_MSG IN out PKG_CR.RefCurType) is<br />
                    <br />
                    --vContent varchar2(4000) := &#39;&#39;;
                    <br />
                    <br />
                    vContent clob;<br />
                    icount integer;<br />
                    vMsg_id number(25);<br />
                    vQuery_ID number(25);<br />
                    vMsg_type varchar2(10);<br />
                    vMsg_name varchar2(150);<br />
                    vField_id varchar2(10);<br />
                    vField_name varchar2(150);<br />
                    vField_value varchar2(4000);<br />
                    fValue01 pkg_cr.cARRAY := pkg_cr.cARRAY();<br />
                    fValue02 pkg_cr.cARRAY := pkg_cr.cARRAY();<br />
                    fValue03 pkg_cr.cARRAY := pkg_cr.cARRAY();<br />
                    fValue04 pkg_cr.cARRAY := pkg_cr.cARRAY();<br />
                    fValue05 pkg_cr.cARRAY := pkg_cr.cARRAY();<br />
                    fValue06 pkg_cr.cARRAY := pkg_cr.cARRAY();<br />
                    fValue07 pkg_cr.cARRAY := pkg_cr.cARRAY();<br />
                    <br />
                    vSender varchar2(50);<br />
                    sendername varchar2(400);<br />
                    vReceiver varchar2(50);<br />
                    receivername varchar2(400);<br />
                    vSession_no varchar2(50);<br />
                    vSeq_no varchar2(50);<br />
                    vStatus varchar2(20);<br />
                    vPriority varchar2(30);<br />
                    vRef_no varchar2(50);<br />
                    vPrint_sts number(1);<br />
                    vReceiving_time date;<br />
                    vSending_time date;<br />
                    vMsgdirection varchar2(20);<br />
                    FieldID pkg_cr.cARRAY := pkg_cr.cARRAY();<br />
                    FieldValue pkg_cr.cARRAY := pkg_cr.cARRAY();<br />
                    vUserID varchar2(100);<br />
                    vUserName varchar2(100);<br />
                    vOrig_Time varchar2(40);<br />
                    BEgin<br />
                    vUserID := pUser;<br />
                    select U.Username into vUserName from Users U where U.Userid=pUser<br />
                    and rownum=1;
                    <br />
                    select count(1)<br />
                    into icount<br />
                    from SWIFT_MSG_CONTENT IMC<br />
                    where IMC.MSG_ID = pMSG_ID;<br />
                    if icount = 0 then<br />
                    select count(1)<br />
                    into icount<br />
                    from SWIFT_Msg_All IMC<br />
                    where IMC.MSG_ID = pMSG_ID;<br />
                    if icount = 0 then<br />
                    select count(1)<br />
                    into icount<br />
                    from SWIFT_Msg_All_His IMC<br />
                    where IMC.MSG_ID = pMSG_ID;<br />
                    if icount &lt;&gt; 0 then<br />
                    -- vTable := &#39;SWIFT_Msg_All_His&#39;;<br />
                    select C.content,<br />
                    msg_id,<br />
                    query_ID,<br />
                    Msg_type,<br />
                    DECODE(c.Msg_Direction,<br />
                    &#39;SIBS-SWIFT&#39;,<br />
                    substr(BRANCH_A, -3),<br />
                    BRANCH_A) AS BRANCH_A,<br />
                    DECODE(c.Msg_Direction,<br />
                    &#39;SIBS-SWIFT&#39;,<br />
                    BSEND1.BANK_NAME,<br />
                    BSEND.BANK_NAME) AS sendername,<br />
                    /* DECODE(c.Msg_Direction,<br />
                    &#39;SWIFT-SIBS&#39;,<br />
                    substr(BRANCH_B, -3),<br />
                    BRANCH_B) AS BRANCH_B,*/<br />
                    C.Bic_Receiver AS BRANCH_B,<br />
                    /*DECODE(c.Msg_Direction,<br />
                    &#39;SWIFT-SIBS&#39;,<br />
                    BRECV1.BANK_NAME,<br />
                    BRECV.BANK_NAME) AS receivername,*/<br />
                    BRECV.BANK_NAME,<br />
                    session_no,<br />
                    osn,<br />
                    DECODE(c.Msg_Direction, &#39;SWIFT-SIBS&#39;, S.Name, PSTS.Content) AS STATUS,<br />
                    &#39;&#39; AS prority,<br />
                    C.Field20,<br />
                    PRINT_STS,<br />
                    receiving_time,<br />
                    sending_time,<br />
                    C.MSG_DIRECTION,<br />
                    (select M.E_MSG_NAME --into vMsg_name<br />
                    from MSGTYPE M<br />
                    where lpad(M.MSG_TYPE, 5, &#39;MT&#39;) LIKE C.Msg_Type<br />
                    and rownum = 1) AS msg_name<br />
                    into vContent,<br />
                    vMsg_id,<br />
                    vQuery_ID,<br />
                    vMsg_type,<br />
                    vSender,<br />
                    sendername,<br />
                    vReceiver,<br />
                    receivername,<br />
                    vSession_no,<br />
                    vSeq_no,<br />
                    vStatus,<br />
                    vPriority,<br />
                    vRef_no,<br />
                    vPrint_sts,<br />
                    vReceiving_time,<br />
                    vSending_time,<br />
                    vMsgdirection,<br />
                    vMsg_name<br />
                    from SWIFT_MSG_ALL_HIS C<br />
                    LEFT JOIN SWIFT_BANK_MAP BSEND on trim(BSEND.SWIFT_BANK_CODE) =<br />
                    C.Branch_a<br />
                    LEFT JOIN SWIFT_BANK_MAP BRECV on rpad(trim(BRECV.SWIFT_BANK_CODE),<br />
                    11,<br />
                    &#39;X&#39;) =<br />
                    rpad(C.Bic_Receiver, 11, &#39;X&#39;)<br />
                    LEFT JOIN SWIFT_BANK_MAP BSEND1 on trim(BSEND1.SIBS_BANK_CODE) =<br />
                    C.Branch_a<br />
                    LEFT JOIN SWIFT_BANK_MAP BRECV1 on rpad(trim(BRECV1.SIBS_BANK_CODE),<br />
                    11,<br />
                    &#39;X&#39;) =<br />
                    rpad(C.Bic_Receiver, 11, &#39;X&#39;)<br />
                    LEFT JOIN ALLCODE PSTS on PSTS.CDVAL = C.Processsts<br />
                    AND PSTS.CDNAME = &#39;PROCESSSTS&#39;<br />
                    AND PSTS.GWTYPE = &#39;SWIFT&#39;<br />
                    LEFT JOIN STATUS S ON C.Status = S.STATUS<br />
                    where msg_id = pMSG_ID;<br />
                    end if;<br />
                    else<br />
                    --vTable := &#39;SWIFT_MSG_All&#39;;<br />
                    select C.content,<br />
                    msg_id,<br />
                    query_ID,<br />
                    Msg_type,<br />
                    DECODE(c.Msg_Direction,<br />
                    &#39;SIBS-SWIFT&#39;,<br />
                    substr(BRANCH_A, -3),<br />
                    BRANCH_A) AS BRANCH_A,<br />
                    DECODE(c.Msg_Direction,<br />
                    &#39;SIBS-SWIFT&#39;,<br />
                    BSEND1.BANK_NAME,<br />
                    BSEND.BANK_NAME) AS sendername,<br />
                    /* DECODE(c.Msg_Direction,<br />
                    &#39;SWIFT-SIBS&#39;,<br />
                    substr(BRANCH_B, -3),<br />
                    BRANCH_B) AS BRANCH_B,<br />
                    DECODE(c.Msg_Direction,<br />
                    &#39;SWIFT-SIBS&#39;,<br />
                    BRECV1.BANK_NAME,<br />
                    BRECV.BANK_NAME) AS receivername,*/<br />
                    C.Bic_Receiver AS BRANCH_B,<br />
                    BRECV.BANK_NAME AS receivername,<br />
                    session_no,<br />
                    osn,<br />
                    DECODE(c.Msg_Direction, &#39;SWIFT-SIBS&#39;, S.Name, PSTS.Content) AS STATUS,<br />
                    &#39;&#39; AS prority,<br />
                    C.Field20,<br />
                    PRINT_STS,<br />
                    receiving_time,<br />
                    sending_time,<br />
                    C.MSG_DIRECTION,<br />
                    (select M.E_MSG_NAME --into vMsg_name<br />
                    from MSGTYPE M<br />
                    where lpad(M.MSG_TYPE, 5, &#39;MT&#39;) LIKE C.Msg_Type<br />
                    and rownum = 1) AS msg_name<br />
                    into vContent,<br />
                    vMsg_id,<br />
                    vQuery_ID,<br />
                    vMsg_type,<br />
                    vSender,<br />
                    sendername,<br />
                    vReceiver,<br />
                    receivername,<br />
                    vSession_no,<br />
                    vSeq_no,<br />
                    vStatus,<br />
                    vPriority,<br />
                    vRef_no,<br />
                    vPrint_sts,<br />
                    vReceiving_time,<br />
                    vSending_time,<br />
                    vMsgdirection,<br />
                    vMsg_name<br />
                    from SWIFT_MSG_All C<br />
                    LEFT JOIN SWIFT_BANK_MAP BSEND on trim(BSEND.SWIFT_BANK_CODE) =<br />
                    C.Branch_a<br />
                    LEFT JOIN SWIFT_BANK_MAP BRECV on rpad(trim(BRECV.SWIFT_BANK_CODE),<br />
                    11,<br />
                    &#39;X&#39;) =<br />
                    rpad(C.Bic_Receiver, 11, &#39;X&#39;)<br />
                    LEFT JOIN SWIFT_BANK_MAP BSEND1 on trim(BSEND1.SIBS_BANK_CODE) =<br />
                    C.Branch_a<br />
                    LEFT JOIN SWIFT_BANK_MAP BRECV1 on rpad(trim(BRECV1.SWIFT_BANK_CODE),<br />
                    11,<br />
                    &#39;X&#39;) =<br />
                    rpad(C.Bic_Receiver, 11, &#39;X&#39;)<br />
                    <br />
                    LEFT JOIN ALLCODE PSTS on PSTS.CDVAL = C.Processsts<br />
                    AND PSTS.CDNAME = &#39;PROCESSSTS&#39;<br />
                    AND PSTS.GWTYPE = &#39;SWIFT&#39;<br />
                    LEFT JOIN STATUS S ON C.Status = S.STATUS<br />
                    where msg_id = pMSG_ID;<br />
                    end if;<br />
                    else<br />
                    -- vTable := &#39;SWIFT_MSG_CONTENT&#39;;<br />
                    select C.content,<br />
                    msg_id,<br />
                    query_ID,<br />
                    Msg_type,<br />
                    DECODE(c.Msg_Direction,<br />
                    &#39;SIBS-SWIFT&#39;,<br />
                    substr(BRANCH_A, -3),<br />
                    BRANCH_A) AS BRANCH_A,<br />
                    DECODE(c.Msg_Direction,<br />
                    &#39;SIBS-SWIFT&#39;,<br />
                    BSEND1.BANK_NAME,<br />
                    BSEND.BANK_NAME) AS sendername,<br />
                    /* DECODE(c.Msg_Direction,<br />
                    &#39;SWIFT-SIBS&#39;,<br />
                    substr(BRANCH_B, -3),<br />
                    BRANCH_B) AS BRANCH_B,<br />
                    DECODE(c.Msg_Direction,<br />
                    &#39;SWIFT-SIBS&#39;,<br />
                    BRECV1.BANK_NAME,<br />
                    BRECV.BANK_NAME) AS receivername,*/<br />
                    C.Bic_Receiver AS BRANCH_B,<br />
                    BRECV.BANK_NAME AS receivername,<br />
                    session_no,<br />
                    osn,<br />
                    DECODE(c.Msg_Direction, &#39;SWIFT-SIBS&#39;, S.Name, PSTS.Content) AS STATUS,<br />
                    &#39;&#39; AS prority,<br />
                    C.Field20,<br />
                    PRINT_STS,<br />
                    receiving_time,<br />
                    sending_time,<br />
                    C.MSG_DIRECTION,<br />
                    (select M.E_MSG_NAME --into vMsg_name<br />
                    from MSGTYPE M<br />
                    where lpad(M.MSG_TYPE, 5, &#39;MT&#39;) LIKE C.Msg_Type<br />
                    and rownum = 1) AS msg_name<br />
                    into vContent,<br />
                    vMsg_id,<br />
                    vQuery_ID,<br />
                    vMsg_type,<br />
                    vSender,<br />
                    sendername,<br />
                    vReceiver,<br />
                    receivername,<br />
                    vSession_no,<br />
                    vSeq_no,<br />
                    vStatus,<br />
                    vPriority,<br />
                    vRef_no,<br />
                    vPrint_sts,<br />
                    vReceiving_time,<br />
                    vSending_time,<br />
                    vMsgdirection,<br />
                    vMsg_name<br />
                    from SWIFT_MSG_CONTENT C<br />
                    LEFT JOIN SWIFT_BANK_MAP BSEND on trim(BSEND.SWIFT_BANK_CODE) =<br />
                    C.Branch_a<br />
                    LEFT JOIN SWIFT_BANK_MAP BRECV on rpad(trim(BRECV.SWIFT_BANK_CODE),<br />
                    11,<br />
                    &#39;X&#39;) =<br />
                    rpad(C.Bic_Receiver, 11, &#39;X&#39;)<br />
                    LEFT JOIN SWIFT_BANK_MAP BSEND1 on trim(BSEND1.SIBS_BANK_CODE) =<br />
                    C.Branch_a<br />
                    LEFT JOIN SWIFT_BANK_MAP BRECV1 on rpad(trim(BRECV1.SWIFT_BANK_CODE),<br />
                    11,<br />
                    &#39;X&#39;) =<br />
                    rpad(C.Bic_Receiver, 11, &#39;X&#39;)<br />
                    LEFT JOIN ALLCODE PSTS on PSTS.CDVAL = C.Processsts<br />
                    AND PSTS.CDNAME = &#39;PROCESSSTS&#39;<br />
                    AND PSTS.GWTYPE = &#39;SWIFT&#39;<br />
                    LEFT JOIN STATUS S ON C.Status = S.STATUS<br />
                    where msg_id = pMSG_ID;<br />
                    end if;<br />
                    <br />
                    delete from SWIFT_REPORT_MSG_TEMP_ALL;<br />
                    <br />
                    GW_PK_SWIFT_REPORT.GET_SWIFT_FIELD_ALL(vContent,<br />
                    FieldID,<br />
                    FieldValue,<br />
                    fValue01,<br />
                    fValue02,<br />
                    fValue03,<br />
                    fValue04,<br />
                    fValue05,<br />
                    fValue06,<br />
                    fValue07);<br />
                    <br />
                    vOrig_Time:=GW_PK_SWIFT_REPORT.GetORG_TIME(vContent);<br />
                    <br />
                    for i in 1 .. FieldID.count loop<br />
                    Insert into SWIFT_REPORT_MSG_TEMP_ALL<br />
                    (msg_id,<br />
                    query_ID,<br />
                    Msg_type,<br />
                    msg_name,<br />
                    field_id,<br />
                    field_name,<br />
                    field_value,<br />
                    sender,<br />
                    sendername,<br />
                    receiver,<br />
                    receivername,<br />
                    session_no,<br />
                    seq_no,<br />
                    STATUS,<br />
                    priority,<br />
                    ref_no,<br />
                    PRINT_STS,<br />
                    receiving_time,<br />
                    sending_time,<br />
                    MSGDIRECTION,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03,<br />
                    EXTFIELD04,<br />
                    EXTFIELD05,<br />
                    EXTFIELD06,<br />
                    EXTFIELD07,<br />
                    EXTFIELD09,<br />
                    EXTFIELD10,<br />
                    ORDERSHOW)<br />
                    select *<br />
                    from (select vMsg_id,<br />
                    vQuery_ID,<br />
                    vMsg_type,<br />
                    vmsg_name,<br />
                    fieldID(i),<br />
                    (Select F.E_FIELD_NAME<br />
                    from msgfield_tf F<br />
                    where lpad(F.MSG_TYPE, length(F.msg_type) + 2, &#39;MT&#39;) =<br />
                    vMsg_type<br />
                    AND &#39;:&#39; || F.FIELD_NAME || &#39;:&#39; like<br />
                    trim(FieldID(i))<br />
                    and rownum = 1) AS fieldname,<br />
                    FieldValue(i),<br />
                    vSender,<br />
                    sendername,<br />
                    vReceiver,<br />
                    receivername,<br />
                    vSession_no,<br />
                    vSeq_no,<br />
                    vStatus,<br />
                    vPriority,<br />
                    vRef_no,<br />
                    vPrint_sts,<br />
                    vReceiving_time,<br />
                    vSending_time,<br />
                    vMsgdirection,<br />
                    fValue01(i),<br />
                    fValue02(i),<br />
                    fValue03(i),<br />
                    fValue04(i),<br />
                    fValue05(i),<br />
                    fValue06(i),<br />
                    fValue07(i),<br />
                    vOrig_Time,<br />
                    vUserName,<br />
                    i<br />
                    from dual);<br />
                    <br />
                    end loop;<br />
                    <br />
                    -- OPen pCurContent for vsql;
                    <br />
                    open pcurBM_SWIFT_MSG for<br />
                    select<br />
                    msg_id,<br />
                    query_ID,<br />
                    Msg_type,<br />
                    msg_name,<br />
                    field_id,<br />
                    field_name,<br />
                    field_value,<br />
                    sender,<br />
                    sendername,<br />
                    receiver,<br />
                    receivername,<br />
                    session_no,<br />
                    seq_no,<br />
                    STATUS,<br />
                    priority,<br />
                    ref_no,<br />
                    PRINT_STS,<br />
                    receiving_time,<br />
                    sending_time,<br />
                    vUserID AS MSGDIRECTION,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    EXTFIELD03,<br />
                    EXTFIELD04,<br />
                    EXTFIELD05,<br />
                    EXTFIELD06,<br />
                    EXTFIELD07,<br />
                    EXTFIELD08,<br />
                    EXTFIELD09,<br />
                    EXTFIELD10,<br />
                    ORDERSHOW<br />
                    from SWIFT_REPORT_MSG_TEMP_ALL<br />
                    order by ORDERSHOW;<br />
                    <br />
                    end SWIFT_PRINT_MSG_LIST;<br />
                    --------------------------<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;SWIFT_PRINT_MSG_TEST&quot; (pMSG_ID IN NUMBER,<br />
                    pMSG_TYPE IN varchar2,<br />
                    pMSGDirection IN varchar2,<br />
                    pCurContent IN out PKG_CR.RefCurType) is<br />
                    <br />
                    --vContent varchar2(4000) := &#39;&#39;;
                    <br />
                    <br />
                    vContent clob;<br />
                    icount integer;<br />
                    vMsg_id number(25);<br />
                    vQuery_ID number(25);<br />
                    vMsg_type varchar2(10);<br />
                    vMsg_name varchar2(150);<br />
                    vField_id varchar2(10);<br />
                    vField_name varchar2(150);<br />
                    vField_value varchar2(4000);<br />
                    vSender varchar2(50);<br />
                    sendername varchar2(400);<br />
                    vReceiver varchar2(50);<br />
                    receivername varchar2(400);<br />
                    vSession_no varchar2(50);<br />
                    vSeq_no varchar2(50);<br />
                    vStatus varchar2(20);<br />
                    vPriority varchar2(30);<br />
                    vRef_no varchar2(50);<br />
                    vPrint_sts number(1);<br />
                    vReceiving_time date;<br />
                    vSending_time date;<br />
                    vMsgdirection varchar2(20);<br />
                    FieldID pkg_cr.cARRAY := pkg_cr.cARRAY();<br />
                    FieldValue pkg_cr.cARRAY := pkg_cr.cARRAY();<br />
                    vUserID varchar2(100);<br />
                    BEgin<br />
                    vUserID := pMSGDirection;<br />
                    select count(1)<br />
                    into icount<br />
                    from SWIFT_MSG_CONTENT IMC<br />
                    where IMC.MSG_ID = pMSG_ID;<br />
                    if icount = 0 then<br />
                    select count(1)<br />
                    into icount<br />
                    from SWIFT_Msg_All IMC<br />
                    where IMC.MSG_ID = pMSG_ID;<br />
                    if icount = 0 then<br />
                    select count(1)<br />
                    into icount<br />
                    from SWIFT_Msg_All_His IMC<br />
                    where IMC.MSG_ID = pMSG_ID;<br />
                    if icount &lt;&gt; 0 then<br />
                    -- vTable := &#39;SWIFT_Msg_All_His&#39;;<br />
                    select C.content,<br />
                    msg_id,<br />
                    query_ID,<br />
                    Msg_type,<br />
                    DECODE(c.Msg_Direction,<br />
                    &#39;SIBS-SWIFT&#39;,<br />
                    substr(BRANCH_A, -3),<br />
                    BRANCH_A) AS BRANCH_A,<br />
                    DECODE(c.Msg_Direction,<br />
                    &#39;SIBS-SWIFT&#39;,<br />
                    BSEND1.BANK_NAME,<br />
                    BSEND.BANK_NAME) AS sendername,<br />
                    /* DECODE(c.Msg_Direction,<br />
                    &#39;SWIFT-SIBS&#39;,<br />
                    substr(BRANCH_B, -3),<br />
                    BRANCH_B) AS BRANCH_B,*/<br />
                    C.Bic_Receiver AS BRANCH_B,<br />
                    /*DECODE(c.Msg_Direction,<br />
                    &#39;SWIFT-SIBS&#39;,<br />
                    BRECV1.BANK_NAME,<br />
                    BRECV.BANK_NAME) AS receivername,*/<br />
                    BRECV.BANK_NAME,<br />
                    session_no,<br />
                    osn,<br />
                    DECODE(c.Msg_Direction, &#39;SWIFT-SIBS&#39;, S.Name, PSTS.Content) AS STATUS,<br />
                    &#39;&#39; AS prority,<br />
                    C.Field20,<br />
                    PRINT_STS,<br />
                    receiving_time,<br />
                    sending_time,<br />
                    C.MSG_DIRECTION,<br />
                    (select M.E_MSG_NAME --into vMsg_name<br />
                    from MSGTYPE M<br />
                    where lpad(M.MSG_TYPE, 5, &#39;MT&#39;) LIKE pMSG_TYPE<br />
                    and rownum = 1) AS msg_name<br />
                    into vContent,<br />
                    vMsg_id,<br />
                    vQuery_ID,<br />
                    vMsg_type,<br />
                    vSender,<br />
                    sendername,<br />
                    vReceiver,<br />
                    receivername,<br />
                    vSession_no,<br />
                    vSeq_no,<br />
                    vStatus,<br />
                    vPriority,<br />
                    vRef_no,<br />
                    vPrint_sts,<br />
                    vReceiving_time,<br />
                    vSending_time,<br />
                    vMsgdirection,<br />
                    vMsg_name<br />
                    from SWIFT_MSG_ALL_HIS C<br />
                    LEFT JOIN SWIFT_BANK_MAP BSEND on trim(BSEND.SWIFT_BANK_CODE) =<br />
                    C.Branch_a<br />
                    LEFT JOIN SWIFT_BANK_MAP BRECV on rpad(trim(BRECV.SWIFT_BANK_CODE),<br />
                    11,<br />
                    &#39;X&#39;) =<br />
                    rpad(C.Bic_Receiver, 11, &#39;X&#39;)<br />
                    LEFT JOIN SWIFT_BANK_MAP BSEND1 on trim(BSEND1.SIBS_BANK_CODE) =<br />
                    C.Branch_a<br />
                    LEFT JOIN SWIFT_BANK_MAP BRECV1 on rpad(trim(BRECV1.SIBS_BANK_CODE),<br />
                    11,<br />
                    &#39;X&#39;) =<br />
                    rpad(C.Bic_Receiver, 11, &#39;X&#39;)<br />
                    LEFT JOIN ALLCODE PSTS on PSTS.CDVAL = C.Processsts<br />
                    AND PSTS.CDNAME = &#39;PROCESSSTS&#39;<br />
                    AND PSTS.GWTYPE = &#39;SWIFT&#39;<br />
                    LEFT JOIN STATUS S ON C.Status = S.STATUS<br />
                    where msg_id = pMSG_ID;<br />
                    end if;<br />
                    else<br />
                    --vTable := &#39;SWIFT_MSG_All&#39;;<br />
                    select C.content,<br />
                    msg_id,<br />
                    query_ID,<br />
                    Msg_type,<br />
                    DECODE(c.Msg_Direction,<br />
                    &#39;SIBS-SWIFT&#39;,<br />
                    substr(BRANCH_A, -3),<br />
                    BRANCH_A) AS BRANCH_A,<br />
                    DECODE(c.Msg_Direction,<br />
                    &#39;SIBS-SWIFT&#39;,<br />
                    BSEND1.BANK_NAME,<br />
                    BSEND.BANK_NAME) AS sendername,<br />
                    /* DECODE(c.Msg_Direction,<br />
                    &#39;SWIFT-SIBS&#39;,<br />
                    substr(BRANCH_B, -3),<br />
                    BRANCH_B) AS BRANCH_B,<br />
                    DECODE(c.Msg_Direction,<br />
                    &#39;SWIFT-SIBS&#39;,<br />
                    BRECV1.BANK_NAME,<br />
                    BRECV.BANK_NAME) AS receivername,*/<br />
                    C.Bic_Receiver AS BRANCH_B,<br />
                    BRECV.BANK_NAME AS receivername,<br />
                    session_no,<br />
                    osn,<br />
                    DECODE(c.Msg_Direction, &#39;SWIFT-SIBS&#39;, S.Name, PSTS.Content) AS STATUS,<br />
                    &#39;&#39; AS prority,<br />
                    C.Field20,<br />
                    PRINT_STS,<br />
                    receiving_time,<br />
                    sending_time,<br />
                    C.MSG_DIRECTION,<br />
                    (select M.E_MSG_NAME --into vMsg_name<br />
                    from MSGTYPE M<br />
                    where lpad(M.MSG_TYPE, 5, &#39;MT&#39;) LIKE pMSG_TYPE<br />
                    and rownum = 1) AS msg_name<br />
                    into vContent,<br />
                    vMsg_id,<br />
                    vQuery_ID,<br />
                    vMsg_type,<br />
                    vSender,<br />
                    sendername,<br />
                    vReceiver,<br />
                    receivername,<br />
                    vSession_no,<br />
                    vSeq_no,<br />
                    vStatus,<br />
                    vPriority,<br />
                    vRef_no,<br />
                    vPrint_sts,<br />
                    vReceiving_time,<br />
                    vSending_time,<br />
                    vMsgdirection,<br />
                    vMsg_name<br />
                    from SWIFT_MSG_All C<br />
                    LEFT JOIN SWIFT_BANK_MAP BSEND on trim(BSEND.SWIFT_BANK_CODE) =<br />
                    C.Branch_a<br />
                    LEFT JOIN SWIFT_BANK_MAP BRECV on rpad(trim(BRECV.SWIFT_BANK_CODE),<br />
                    11,<br />
                    &#39;X&#39;) =<br />
                    rpad(C.Bic_Receiver, 11, &#39;X&#39;)<br />
                    LEFT JOIN SWIFT_BANK_MAP BSEND1 on trim(BSEND1.SIBS_BANK_CODE) =<br />
                    C.Branch_a<br />
                    LEFT JOIN SWIFT_BANK_MAP BRECV1 on rpad(trim(BRECV1.SWIFT_BANK_CODE),<br />
                    11,<br />
                    &#39;X&#39;) =<br />
                    rpad(C.Bic_Receiver, 11, &#39;X&#39;)<br />
                    <br />
                    LEFT JOIN ALLCODE PSTS on PSTS.CDVAL = C.Processsts<br />
                    AND PSTS.CDNAME = &#39;PROCESSSTS&#39;<br />
                    AND PSTS.GWTYPE = &#39;SWIFT&#39;<br />
                    LEFT JOIN STATUS S ON C.Status = S.STATUS<br />
                    where msg_id = pMSG_ID;<br />
                    end if;<br />
                    else<br />
                    -- vTable := &#39;SWIFT_MSG_CONTENT&#39;;<br />
                    select C.content,<br />
                    msg_id,<br />
                    query_ID,<br />
                    Msg_type,<br />
                    DECODE(c.Msg_Direction,<br />
                    &#39;SIBS-SWIFT&#39;,<br />
                    substr(BRANCH_A, -3),<br />
                    BRANCH_A) AS BRANCH_A,<br />
                    DECODE(c.Msg_Direction,<br />
                    &#39;SIBS-SWIFT&#39;,<br />
                    BSEND1.BANK_NAME,<br />
                    BSEND.BANK_NAME) AS sendername,<br />
                    /* DECODE(c.Msg_Direction,<br />
                    &#39;SWIFT-SIBS&#39;,<br />
                    substr(BRANCH_B, -3),<br />
                    BRANCH_B) AS BRANCH_B,<br />
                    DECODE(c.Msg_Direction,<br />
                    &#39;SWIFT-SIBS&#39;,<br />
                    BRECV1.BANK_NAME,<br />
                    BRECV.BANK_NAME) AS receivername,*/<br />
                    C.Bic_Receiver AS BRANCH_B,<br />
                    BRECV.BANK_NAME AS receivername,<br />
                    session_no,<br />
                    osn,<br />
                    DECODE(c.Msg_Direction, &#39;SWIFT-SIBS&#39;, S.Name, PSTS.Content) AS STATUS,<br />
                    &#39;&#39; AS prority,<br />
                    C.Field20,<br />
                    PRINT_STS,<br />
                    receiving_time,<br />
                    sending_time,<br />
                    C.MSG_DIRECTION,<br />
                    (select M.E_MSG_NAME --into vMsg_name<br />
                    from MSGTYPE M<br />
                    where lpad(M.MSG_TYPE, 5, &#39;MT&#39;) LIKE pMSG_TYPE<br />
                    and rownum = 1) AS msg_name<br />
                    into vContent,<br />
                    vMsg_id,<br />
                    vQuery_ID,<br />
                    vMsg_type,<br />
                    vSender,<br />
                    sendername,<br />
                    vReceiver,<br />
                    receivername,<br />
                    vSession_no,<br />
                    vSeq_no,<br />
                    vStatus,<br />
                    vPriority,<br />
                    vRef_no,<br />
                    vPrint_sts,<br />
                    vReceiving_time,<br />
                    vSending_time,<br />
                    vMsgdirection,<br />
                    vMsg_name<br />
                    from SWIFT_MSG_CONTENT C<br />
                    LEFT JOIN SWIFT_BANK_MAP BSEND on trim(BSEND.SWIFT_BANK_CODE) =<br />
                    C.Branch_a<br />
                    LEFT JOIN SWIFT_BANK_MAP BRECV on rpad(trim(BRECV.SWIFT_BANK_CODE),<br />
                    11,<br />
                    &#39;X&#39;) =<br />
                    rpad(C.Bic_Receiver, 11, &#39;X&#39;)<br />
                    LEFT JOIN SWIFT_BANK_MAP BSEND1 on trim(BSEND1.SIBS_BANK_CODE) =<br />
                    C.Branch_a<br />
                    LEFT JOIN SWIFT_BANK_MAP BRECV1 on rpad(trim(BRECV1.SWIFT_BANK_CODE),<br />
                    11,<br />
                    &#39;X&#39;) =<br />
                    rpad(C.Bic_Receiver, 11, &#39;X&#39;)<br />
                    LEFT JOIN ALLCODE PSTS on PSTS.CDVAL = C.Processsts<br />
                    AND PSTS.CDNAME = &#39;PROCESSSTS&#39;<br />
                    AND PSTS.GWTYPE = &#39;SWIFT&#39;<br />
                    LEFT JOIN STATUS S ON C.Status = S.STATUS<br />
                    where msg_id = pMSG_ID;<br />
                    end if;<br />
                    <br />
                    delete from SWIFT_REPORT_MSG_TEMP;<br />
                    <br />
                    GW_PK_SWIFT_REPORT.SPLIT_SWIFT_FIELD_ORDER(vContent,<br />
                    FieldID,<br />
                    FieldValue);<br />
                    <br />
                    for i in 1 .. FieldID.count loop<br />
                    Insert into SWIFT_REPORT_MSG_TEMP<br />
                    (msg_id,<br />
                    query_ID,<br />
                    Msg_type,<br />
                    msg_name,<br />
                    field_id,<br />
                    field_name,<br />
                    field_value,<br />
                    sender,<br />
                    sendername,<br />
                    receiver,<br />
                    receivername,<br />
                    session_no,<br />
                    seq_no,<br />
                    STATUS,<br />
                    priority,<br />
                    ref_no,<br />
                    PRINT_STS,<br />
                    receiving_time,<br />
                    sending_time,<br />
                    MSGDIRECTION,<br />
                    ORDERSHOW)<br />
                    select *<br />
                    from (select vMsg_id,<br />
                    vQuery_ID,<br />
                    vMsg_type,<br />
                    vmsg_name,<br />
                    fieldID(i),<br />
                    (Select F.E_FIELD_NAME<br />
                    from msgfield_tf F<br />
                    where lpad(F.MSG_TYPE, length(F.msg_type) + 2, &#39;MT&#39;) =<br />
                    vMsg_type<br />
                    AND &#39;:&#39; || F.FIELD_NAME || &#39;:&#39; like<br />
                    trim(FieldID(i))<br />
                    and rownum = 1) AS fieldname,<br />
                    FieldValue(i),<br />
                    vSender,<br />
                    sendername,<br />
                    vReceiver,<br />
                    receivername,<br />
                    vSession_no,<br />
                    vSeq_no,<br />
                    vStatus,<br />
                    vPriority,<br />
                    vRef_no,<br />
                    vPrint_sts,<br />
                    vReceiving_time,<br />
                    vSending_time,<br />
                    vMsgdirection,<br />
                    i<br />
                    from dual);<br />
                    <br />
                    end loop;<br />
                    <br />
                    -- OPen pCurContent for vsql;
                    <br />
                    open pCurContent for<br />
                    select msg_id,<br />
                    query_ID,<br />
                    Msg_type,<br />
                    msg_name,<br />
                    field_id,<br />
                    field_name,<br />
                    field_value,<br />
                    sender,<br />
                    sendername,<br />
                    receiver,<br />
                    receivername,<br />
                    session_no,<br />
                    seq_no,<br />
                    STATUS,<br />
                    priority,<br />
                    ref_no,<br />
                    PRINT_STS,<br />
                    receiving_time,<br />
                    sending_time,<br />
                    vUserID AS MSGDIRECTION,<br />
                    EXTFIELD01,<br />
                    EXTFIELD02,<br />
                    ORDERSHOW<br />
                    from SWIFT_REPORT_MSG_TEMP<br />
                    order by ORDERSHOW;<br />
                    <br />
                    end SWIFT_PRINT_MSG_TEST;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;TEST_PRO&quot; (pdate IN date,<br />
                    pCCYCD in varchar2 default &#39;VND&#39;,<br />
                    pBranchA in varchar2,<br />
                    pBranchB in varchar2,<br />
                    RefCurVCB_03 IN OUT PKG_CR.RefCurType) IS<br />
                    vCCYCD varchar2(3);<br />
                    vBranchA varchar2(12);<br />
                    vBranchB varchar2(12);<br />
                    BEGIN<br />
                    if upper(trim(pCCYCD)) = &#39;ALL&#39; or trim(pCCYCD) is null then<br />
                    vCCYCD := &#39;%&#39;;<br />
                    else<br />
                    vCCYCD := trim(pCCYCD);<br />
                    end if;<br />
                    if upper(trim(pBranchA)) = &#39;ALL&#39; or trim(pBranchA) is null then<br />
                    vBranchA := &#39;%&#39;;<br />
                    else<br />
                    vBranchA := substr(trim(pBranchA), -3);<br />
                    <br />
                    end if;<br />
                    if upper(trim(pBranchB)) = &#39;ALL&#39; or trim(pBranchB) is null then<br />
                    vBranchB := &#39;%&#39;;<br />
                    else<br />
                    vBranchB := substr(trim(pBranchB), -3);<br />
                    end if;<br />
                    <br />
                    open RefCurVCB_03 for<br />
                    SELECT AR.*<br />
                    FROM (select R.*, b.content AS STATUSNAME<br />
                    from (SELECT A.msg_id,<br />
                    NVL(A.RM_NUMBER, &#39;0&#39;) as RM_NUMBER,<br />
                    /*CASE A.MSG_DIRECTION<br />
                    WHEN &#39;I&#39; THEN<br />
                    CASE trim(C.CONTENT)<br />
                    WHEN null THEN<br />
                    CASE trim(CA.CONTENT)<br />
                    WHEN null THEN<br />
                    VCB_GET_Field(CH.CONTENT, &#39;72&#39;, CH.Msg_Type)<br />
                    ELSE<br />
                    VCB_GET_Field(CA.CONTENT, &#39;72&#39;, CA.Msg_Type)<br />
                    END ELSE VCB_GET_Field(C.CONTENT, &#39;72&#39;, C.MSG_TYPE) END ELSE*/ -- DIEN DI<br />
                    A.Trans_No AS Ref_no, -- truong 72<br />
                    A.ref_number AS Field20, -- so lenh<br />
                    A.sender,<br />
                    A.receiver,<br />
                    A.amount,<br />
                    A.gw_type sibs_ttsp,<br />
                    A.trans_date,<br />
                    A.ccy,<br />
                    A.exception_type,<br />
                    A.msg_direction,<br />
                    decode(trim(C.Content),<br />
                    NULL,<br />
                    decode(CA.Content,<br />
                    NULL,<br />
                    CH.Status,<br />
                    CA.Status),<br />
                    C.Status) AS STATUS<br />
                    /*
                    <br />
                    CASE trim(C.CONTENT)<br />
                    WHEN null THEN<br />
                    CASE trim(CA.CONTENT)<br />
                    WHEN null THEN<br />
                    CH.Status<br />
                    ELSE<br />
                    CA.Status<br />
                    END
                    <br />
                    ELSE C.Status
                    <br />
                    END AS STATUS*/<br />
                    FROM VCB_MSG_REC A<br />
                    LEFT JOIN VCB_MSG_CONTENT C<br />
                    ON C.QUERY_ID = A.Query_Id<br />
                    LEFT JOIN VCB_MSG_ALL CA<br />
                    ON CA.QUERY_ID = A.Query_Id<br />
                    LEFT JOIN VCB_MSG_ALL_HIS CH<br />
                    ON CH.QUERY_ID = A.Query_Id<br />
                    WHERE nvl(a.ccy, &#39;%&#39;) like vCCYCD<br />
                    AND to_char(A.trans_date, &#39;DDMMYYYY&#39;) =<br />
                    TO_char(pdate, &#39;DDMMYYYY&#39;)<br />
                    AND nvl(substr(A.sender, -3), &#39;%&#39;) like vBranchA<br />
                    AND nvl(substr(A.receiver, -3), &#39;%&#39;) like vBranchB<br />
                    <br />
                    ) R<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B<br />
                    ON trim(R.STATUS) = trim(B.STATUS)<br />
                    <br />
                    ) AR<br />
                    --uncomment de chuyen sang bao cao doi chieu ko co dien lap GV-V<br />
                    WHERE AR.msg_id not in
                    <br />
                    (
                    <br />
                    select R.Msg_Id from
                    <br />
                    (<br />
                    select A.msg_id,<br />
                    (select B.Amount from vcb_msg_rec B where B.Ref_Number=A.Ref_Number AND 
                    B.Exception_Type=&#39;V&#39; and rownum=1) AS VCB_AMOUNT<br />
                    from vcb_msg_rec A where A.Exception_Type=&#39;GV&#39; AND 
                    to_char(A.Trans_Date,&#39;DDMMYYYY&#39;)=TO_char(pdate, &#39;DDMMYYYY&#39;)<br />
                    ) R<br />
                    where trim(R.vcb_amount) is not null<br />
                    union<br />
                    select R.Msg_Id from
                    <br />
                    (<br />
                    select A.msg_id,<br />
                    (select B.Amount from vcb_msg_rec B where B.Ref_Number=A.Ref_Number AND 
                    B.Exception_Type=&#39;GV&#39; and rownum=1) AS VCB_AMOUNT<br />
                    from vcb_msg_rec A where A.Exception_Type=&#39;V&#39; AND 
                    to_char(A.Trans_Date,&#39;DDMMYYYY&#39;)=TO_char(pdate, &#39;DDMMYYYY&#39;)<br />
                    ) R<br />
                    where trim(R.vcb_amount) is not null<br />
                    )<br />
                    ORDER BY AR.field20, AR.msg_direction, AR.RM_NUMBER;<br />
                    <br />
                    <br />
                    end TEST_PRO;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;TLLOG&quot; (strSQL varchar2,<br />
                    host varchar2,<br />
                    user varchar2,<br />
                    pwd varchar2,<br />
                    cp_cursor out SYS_REFCURSOR) AS<br />
                    LANGUAGE JAVA NAME &#39;Gettllog.getRS(java.lang.String,java.lang.String, 
                    java.lang.String, java.lang.String,java.sql.ResultSet[]) &#39;;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;VCB_04&quot; (pDate in date,pBRANCHA in 
                    VARCHAR2,pBRANCHB in VARCHAR2,pCCYCD in VARCHAR2,pStatus in 
                    VARCHAR2,RefCurVCB_04 IN OUT PKG_CR.RefCurType) IS<br />
                    <br />
                    vCCYCD VARCHAR2(3);<br />
                    vOndate date;<br />
                    vStatus VARCHAR2(50);<br />
                    vBranchA VARCHAR2(12);<br />
                    vBranchB VARCHAR2(12);<br />
                    BEGIN<br />
                    --select sysdate into vOndate from dual;<br />
                    --select to_char(sysdate,&#39;dd/mm/yyyy&#39;) from dual;<br />
                    select sysdate into vOndate from dual;<br />
                    if upper(trim(pCCYCD)) = &#39;ALL&#39; then<br />
                    vCCYCD := &#39;%&#39;;<br />
                    else<br />
                    vCCYCD := trim(pCCYCD);<br />
                    end if;<br />
                    if upper(trim(pStatus)) = &#39;ALL&#39; then<br />
                    vStatus := &#39;%&#39;;<br />
                    else<br />
                    vStatus := trim(pStatus);<br />
                    end if;<br />
                    if upper(trim(pBRANCHA)) = &#39;ALL&#39; then<br />
                    vBranchA := &#39;%&#39;;<br />
                    else<br />
                    vBranchA := trim(lpad(pBRANCHA, 5, 0));<br />
                    end if;<br />
                    if upper(trim(pBRANCHB)) = &#39;ALL&#39; then<br />
                    vBranchB := &#39;%&#39;;<br />
                    else<br />
                    vBranchB := trim(pBRANCHB);<br />
                    end if;<br />
                    <br />
                    if to_date(pDate, &#39;DD/MM/YYYY&#39;) = to_date(vOndate, &#39;DD/MM/YYYY&#39;) then<br />
                    --open RefCurBM_IBPS02 for<br />
                    delete from VCB_04_TEMP;<br />
                    insert into VCB_04_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    RM_NUMBER,<br />
                    FIELD20,<br />
                    BRANCH_B,<br />
                    AMOUNT,<br />
                    MSG_TYPE,<br />
                    STATUS,<br />
                    BRANCH_A,<br />
                    CCYCD,<br />
                    TRANS_DATE,<br />
                    SENDER,<br />
                    RECEIVER)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.RM_NUMBER,<br />
                    A.FIELD20,<br />
                    A.BRANCH_B,<br />
                    A.AMOUNT,<br />
                    A.MSG_TYPE,<br />
                    B.CONTENT AS STATUS,<br />
                    A.BRANCH_A,<br />
                    A.CCYCD,<br />
                    A.TRANS_DATE,<br />
                    A.SENDER,<br />
                    A.RECEIVER<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    --C.RM_NUMBER,<br />
                    substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,<br />
                    C.FIELD20,<br />
                    BRANCH_B,<br />
                    C.AMOUNT,<br />
                    C.MSG_TYPE,<br />
                    C.STATUS,<br />
                    C.BRANCH_A,<br />
                    C.CCYCD,<br />
                    C.TRANS_DATE,<br />
                    GW_PK_VCB_REPORT.VCB_GET_FIELD(C.CONTENT, &#39;50K&#39;, C.MSG_TYPE) AS SENDER,<br />
                    GW_PK_VCB_REPORT.VCB_GET_FIELD(C.CONTENT, &#39;72&#39;, C.MSG_TYPE) AS RECEIVER<br />
                    FROM VCB_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-VCB&#39;<br />
                    AND c.branch_a LIKE vBranchA<br />
                    AND c.branch_b like vBranchB<br />
                    AND c.msg_src not in (&#39;2&#39;, &#39;3&#39;)<br />
                    AND C.ccycd LIKE vCcycd) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    WHERE B.CONTENT LIKE vStatus;<br />
                    <br />
                    elsif substr(to_char(to_date(pDate, &#39;DD/MM/YYYY&#39;), &#39;DD/MM/YYYY&#39;), 4, 2) =<br />
                    substr(to_char(to_date(vOndate, &#39;DD/MM/YYYY&#39;), &#39;DD/MM/YYYY&#39;),<br />
                    4,<br />
                    2) then<br />
                    delete from VCB_04_TEMP;<br />
                    --lay du lieu ngay<br />
                    insert into VCB_04_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    RM_NUMBER,<br />
                    FIELD20,<br />
                    BRANCH_B,<br />
                    AMOUNT,<br />
                    MSG_TYPE,<br />
                    STATUS,<br />
                    BRANCH_A,<br />
                    CCYCD,<br />
                    TRANS_DATE,<br />
                    SENDER,<br />
                    RECEIVER)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.RM_NUMBER,<br />
                    A.FIELD20,<br />
                    A.BRANCH_B,<br />
                    A.AMOUNT,<br />
                    A.MSG_TYPE,<br />
                    B.CONTENT AS STATUS,<br />
                    A.BRANCH_A,<br />
                    A.CCYCD,<br />
                    A.TRANS_DATE,<br />
                    A.SENDER,<br />
                    A.RECEIVER<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    --C.RM_NUMBER,<br />
                    substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,<br />
                    C.FIELD20,<br />
                    BRANCH_B,<br />
                    C.AMOUNT,<br />
                    C.MSG_TYPE,<br />
                    C.STATUS,<br />
                    C.BRANCH_A,<br />
                    C.CCYCD,<br />
                    C.TRANS_DATE,<br />
                    GW_PK_VCB_REPORT.VCB_GET_FIELD(C.CONTENT, &#39;50K&#39;, C.MSG_TYPE) AS SENDER,<br />
                    GW_PK_VCB_REPORT.VCB_GET_FIELD(C.CONTENT, &#39;72&#39;, C.MSG_TYPE) AS RECEIVER<br />
                    FROM VCB_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-VCB&#39;<br />
                    AND c.branch_a LIKE vBranchA<br />
                    AND c.branch_b like vBranchB<br />
                    AND c.msg_src not in (&#39;2&#39;, &#39;3&#39;)<br />
                    AND C.ccycd LIKE vCcycd) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    WHERE B.CONTENT LIKE vStatus;<br />
                    --lay du lieu trong thang<br />
                    insert into VCB_04_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    RM_NUMBER,<br />
                    FIELD20,<br />
                    BRANCH_B,<br />
                    AMOUNT,<br />
                    MSG_TYPE,<br />
                    STATUS,<br />
                    BRANCH_A,<br />
                    CCYCD,<br />
                    TRANS_DATE,<br />
                    SENDER,<br />
                    RECEIVER)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.RM_NUMBER,<br />
                    A.FIELD20,<br />
                    A.BRANCH_B,<br />
                    A.AMOUNT,<br />
                    A.MSG_TYPE,<br />
                    B.CONTENT AS STATUS,<br />
                    A.BRANCH_A,<br />
                    A.CCYCD,<br />
                    A.TRANS_DATE,<br />
                    A.SENDER,<br />
                    A.RECEIVER<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    --C.RM_NUMBER,<br />
                    substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,<br />
                    C.FIELD20,<br />
                    BRANCH_B,<br />
                    C.AMOUNT,<br />
                    C.MSG_TYPE,<br />
                    C.STATUS,<br />
                    C.BRANCH_A,<br />
                    C.CCYCD,<br />
                    C.TRANS_DATE,<br />
                    GW_PK_VCB_REPORT.VCB_GET_FIELD(C.CONTENT, &#39;50K&#39;, C.MSG_TYPE) AS SENDER,<br />
                    GW_PK_VCB_REPORT.VCB_GET_FIELD(C.CONTENT, &#39;72&#39;, C.MSG_TYPE) AS RECEIVER<br />
                    FROM VCB_MSG_ALL C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-VCB&#39;<br />
                    AND c.branch_a LIKE vBranchA<br />
                    AND c.branch_b like vBranchB<br />
                    AND c.msg_src not in (&#39;2&#39;, &#39;3&#39;)<br />
                    AND C.ccycd LIKE vCcycd) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    WHERE B.CONTENT LIKE vStatus;<br />
                    else<br />
                    delete from VCB_04_TEMP;<br />
                    --lay du lieu ngay<br />
                    insert into VCB_04_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    RM_NUMBER,<br />
                    FIELD20,<br />
                    BRANCH_B,<br />
                    AMOUNT,<br />
                    MSG_TYPE,<br />
                    STATUS,<br />
                    BRANCH_A,<br />
                    CCYCD,<br />
                    TRANS_DATE,<br />
                    SENDER,<br />
                    RECEIVER)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.RM_NUMBER,<br />
                    A.FIELD20,<br />
                    A.BRANCH_B,<br />
                    A.AMOUNT,<br />
                    A.MSG_TYPE,<br />
                    B.CONTENT AS STATUS,<br />
                    A.BRANCH_A,<br />
                    A.CCYCD,<br />
                    A.TRANS_DATE,<br />
                    A.SENDER,<br />
                    A.RECEIVER<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    --C.RM_NUMBER,<br />
                    substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,<br />
                    C.FIELD20,<br />
                    BRANCH_B,<br />
                    C.AMOUNT,<br />
                    C.MSG_TYPE,<br />
                    C.STATUS,<br />
                    C.BRANCH_A,<br />
                    C.CCYCD,<br />
                    C.TRANS_DATE,<br />
                    GW_PK_VCB_REPORT.VCB_GET_FIELD(C.CONTENT, &#39;50K&#39;, C.MSG_TYPE) AS SENDER,<br />
                    GW_PK_VCB_REPORT.VCB_GET_FIELD(C.CONTENT, &#39;72&#39;, C.MSG_TYPE) AS RECEIVER<br />
                    FROM VCB_MSG_CONTENT C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-VCB&#39;<br />
                    AND c.branch_a LIKE vBranchA<br />
                    AND c.branch_b like vBranchB<br />
                    AND c.msg_src not in (&#39;2&#39;, &#39;3&#39;)<br />
                    AND C.ccycd LIKE vCcycd) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    WHERE B.CONTENT LIKE vStatus;<br />
                    --lay du lieu trong thang<br />
                    insert into VCB_04_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    RM_NUMBER,<br />
                    FIELD20,<br />
                    BRANCH_B,<br />
                    AMOUNT,<br />
                    MSG_TYPE,<br />
                    STATUS,<br />
                    BRANCH_A,<br />
                    CCYCD,<br />
                    TRANS_DATE,<br />
                    SENDER,<br />
                    RECEIVER)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.RM_NUMBER,<br />
                    A.FIELD20,<br />
                    A.BRANCH_B,<br />
                    A.AMOUNT,<br />
                    A.MSG_TYPE,<br />
                    B.CONTENT AS STATUS,<br />
                    A.BRANCH_A,<br />
                    A.CCYCD,<br />
                    A.TRANS_DATE,<br />
                    A.SENDER,<br />
                    A.RECEIVER<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    --C.RM_NUMBER,<br />
                    substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,<br />
                    C.FIELD20,<br />
                    BRANCH_B,<br />
                    C.AMOUNT,<br />
                    C.MSG_TYPE,<br />
                    C.STATUS,<br />
                    C.BRANCH_A,<br />
                    C.CCYCD,<br />
                    C.TRANS_DATE,<br />
                    GW_PK_VCB_REPORT.VCB_GET_FIELD(C.CONTENT, &#39;50K&#39;, C.MSG_TYPE) AS SENDER,<br />
                    GW_PK_VCB_REPORT.VCB_GET_FIELD(C.CONTENT, &#39;72&#39;, C.MSG_TYPE) AS RECEIVER<br />
                    FROM VCB_MSG_ALL C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-VCB&#39;<br />
                    AND c.branch_a LIKE vBranchA<br />
                    AND c.branch_b like vBranchB<br />
                    AND c.msg_src not in (&#39;2&#39;, &#39;3&#39;)<br />
                    AND C.ccycd LIKE vCcycd) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    WHERE B.CONTENT LIKE vStatus;<br />
                    --lay du lieu trong nam<br />
                    insert into VCB_04_TEMP<br />
                    (MSG_ID,<br />
                    QUERY_ID,<br />
                    RM_NUMBER,<br />
                    FIELD20,<br />
                    BRANCH_B,<br />
                    AMOUNT,<br />
                    MSG_TYPE,<br />
                    STATUS,<br />
                    BRANCH_A,<br />
                    CCYCD,<br />
                    TRANS_DATE,<br />
                    SENDER,<br />
                    RECEIVER)<br />
                    SELECT A.MSG_ID,<br />
                    A.QUERY_ID,<br />
                    A.RM_NUMBER,<br />
                    A.FIELD20,<br />
                    A.BRANCH_B,<br />
                    A.AMOUNT,<br />
                    A.MSG_TYPE,<br />
                    B.CONTENT AS STATUS,<br />
                    A.BRANCH_A,<br />
                    A.CCYCD,<br />
                    A.TRANS_DATE,<br />
                    A.SENDER,<br />
                    A.RECEIVER<br />
                    FROM (SELECT C.MSG_ID,<br />
                    C.QUERY_ID,<br />
                    --C.RM_NUMBER,<br />
                    substr(C.RM_NUMBER, 5, length(C.RM_NUMBER) - 4) AS RM_NUMBER,<br />
                    C.FIELD20,<br />
                    BRANCH_B,<br />
                    C.AMOUNT,<br />
                    C.MSG_TYPE,<br />
                    C.STATUS,<br />
                    C.BRANCH_A,<br />
                    C.CCYCD,<br />
                    C.TRANS_DATE,<br />
                    GW_PK_VCB_REPORT.VCB_GET_FIELD(C.CONTENT, &#39;50K&#39;, C.MSG_TYPE) AS SENDER,<br />
                    GW_PK_VCB_REPORT.VCB_GET_FIELD(C.CONTENT, &#39;72&#39;, C.MSG_TYPE) AS RECEIVER<br />
                    FROM VCB_MSG_ALL_HIS C<br />
                    WHERE C.TRANSDATE = to_char(pDate, &#39;YYYYMMDD&#39;)<br />
                    AND trim(C.MSG_DIRECTION) = &#39;SIBS-VCB&#39;<br />
                    AND c.branch_a LIKE vBranchA<br />
                    AND c.branch_b like vBranchB<br />
                    AND c.msg_src not in (&#39;2&#39;, &#39;3&#39;)<br />
                    AND C.ccycd LIKE vCcycd) A<br />
                    LEFT JOIN (SELECT NAME as CONTENT, STATUS FROM STATUS) B ON trim(A.STATUS) =<br />
                    trim(B.STATUS)<br />
                    WHERE B.CONTENT LIKE vStatus;<br />
                    <br />
                    end if;<br />
                    open RefCurVCB_04 for<br />
                    --select * from VCB_04_TEMP order by QUERY_ID,RM_NUMBER asc;<br />
                    select 
                    MSG_ID,QUERY_ID,RM_NUMBER,FIELD20,BRANCH_B,AMOUNT,MSG_TYPE,STATUS,substr(BRANCH_A, 
                    -3) as BRANCH_A,CCYCD,TRANS_DATE,SENDER,RECEIVER,&#39;aasa&#39; AS AAA,&#39;BBB&#39; AS BBBB<br />
                    from VCB_04_TEMP<br />
                    order by QUERY_ID, RM_NUMBER asc;<br />
                    END VCB_04;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;VCB_FEE_CAL&quot; (pFromDate in date,<br />
                    pToDate in date,<br />
                    pBranch in varchar,<br />
                    pFeeType in number,<br />
                    pCCYCD in varchar,<br />
                    RefCurBK02 IN OUT PKG_CR.RefCurType) IS
                    <br />
                    vCCYCD varchar2(5);<br />
                    vBRANCH varchar2(5);<br />
                    strSql varchar2(4000);<br />
                    vFR_DATE NUMBER(10);<br />
                    vTO_DATE NUMBER(10);<br />
                    BEGIN<br />
                    vFR_DATE := TO_NUMBER(TO_CHAR(pFromDate, &#39;YYYYMMDD&#39;));<br />
                    vTO_DATE := TO_NUMBER(TO_CHAR(pToDate, &#39;YYYYMMDD&#39;));
                    <br />
                    if upper(trim(pBranch)) = &#39;ALL&#39; or trim(pBranch) is null then<br />
                    vBRANCH := &#39;%&#39;;<br />
                    else<br />
                    vBRANCH := LPAD(pBranch, 5, &#39;0&#39;);<br />
                    end if;
                    <br />
                    if upper(trim(pCCYCD)) = &#39;ALL&#39; or trim(pCCYCD) is null then<br />
                    vCCYCD := &#39;%&#39;;<br />
                    else<br />
                    vCCYCD := trim(pCCYCD);<br />
                    end if;
                    <br />
                    --XOA DU LIEU BANG TEMP<br />
                    DELETE FROM VCB_FEE_CAL_TEMP;<br />
                    ----------------------------------<br />
                    --INSERT DU LIEU VAO BANG TEMP
                    <br />
                    INSERT INTO VCB_FEE_CAL_TEMP<br />
                    (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)<br />
                    SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT,
                    <br />
                    decode(msg_src,2,branch_a,<br />
                    lpad(to_number(substr(RM_NUMBER,1, length(RM_NUMBER)-12)),5,&#39;0&#39;))
                    <br />
                    FROM VCB_MSG_CONTENT<br />
                    WHERE TRANSDATE &gt;= vFR_DATE<br />
                    AND TRANSDATE &lt;= vTO_DATE<br />
                    AND STATUS = 1<br />
                    AND BRANCH_A LIKE vBRANCH<br />
                    AND CCYCD LIKE vCCYCD
                    <br />
                    AND MSG_DIRECTION = &#39;SIBS-VCB&#39;;<br />
                    INSERT INTO VCB_FEE_CAL_TEMP<br />
                    (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)<br />
                    SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT,
                    <br />
                    decode(msg_src,2,branch_a,<br />
                    lpad(to_number(substr(RM_NUMBER,1, length(RM_NUMBER)-12)),5,&#39;0&#39;))
                    <br />
                    FROM VCB_MSG_ALL<br />
                    WHERE TRANSDATE &gt;= vFR_DATE<br />
                    AND TRANSDATE &lt;= vTO_DATE<br />
                    AND STATUS = 1<br />
                    AND BRANCH_A LIKE vBRANCH<br />
                    AND CCYCD LIKE vCCYCD
                    <br />
                    AND MSG_DIRECTION = &#39;SIBS-VCB&#39;;<br />
                    INSERT INTO VCB_FEE_CAL_TEMP<br />
                    (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)<br />
                    SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT,
                    <br />
                    decode(msg_src,2,branch_a,<br />
                    lpad(to_number(substr(RM_NUMBER,1, length(RM_NUMBER)-12)),5,&#39;0&#39;))
                    <br />
                    FROM VCB_MSG_ALL_HIS<br />
                    WHERE TRANSDATE &gt;= vFR_DATE<br />
                    AND TRANSDATE &lt;= vTO_DATE<br />
                    AND STATUS = 1<br />
                    AND BRANCH_A LIKE vBRANCH<br />
                    AND CCYCD LIKE vCCYCD
                    <br />
                    AND MSG_DIRECTION = &#39;SIBS-VCB&#39;;<br />
                    --TINH PHI VOI LOAI PHI CO DINH<br />
                    IF pFeeType = 1 THEN<br />
                    strSql := &#39;SELECT SUM(T.AMOUNT)AS FEE,COUNT(T.MSG_ID) AS NUM,
                    <br />
                    K.BRAN_NAME, T.BRANCH, T.CCYCD<br />
                    FROM<br />
                    (SELECT A.MSG_ID, NVL(B.FIXEDFEE,0) AMOUNT,A.BRANCH,A.CCYCD
                    <br />
                    FROM VCB_FEE_CAL_TEMP A INNER JOIN
                    <br />
                    VCB_FEE B ON A.CCYCD=B.CCYCD
                    <br />
                    )T INNER JOIN BRANCH K ON
                    <br />
                    LPAD(T.BRANCH,5,&#39;&#39;0&#39;&#39;) = LPAD(K.SIBS_BANK_CODE,5,&#39;&#39;0&#39;&#39;)<br />
                    GROUP BY K.BRAN_NAME, T.BRANCH, T.CCYCD
                    <br />
                    ORDER BY K.BRAN_NAME&#39;;<br />
                    --TINH PHI THEO TY LE<br />
                    ELSE<br />
                    strSql := &#39;SELECT SUM(T.AMOUNT)AS FEE,COUNT(T.MSG_ID) AS NUM,
                    <br />
                    K.BRAN_NAME, T.BRANCH, T.CCYCD<br />
                    FROM<br />
                    (SELECT A.MSG_ID,(CASE WHEN NVL(B.RATE,0) * A.AMOUNT/100&gt;= NVL(B.MAXFEE,0) THEN
                    <br />
                    NVL(B.MAXFEE,0) WHEN NVL(B.RATE,0) * A.AMOUNT/100&lt;= NVL(B.MINFEE,0) THEN
                    <br />
                    NVL(B.MINFEE,0) ELSE NVL(B.RATE,0) * A.AMOUNT/100 END)as AMOUNT,<br />
                    A.BRANCH,A.CCYCD
                    <br />
                    FROM VCB_FEE_CAL_TEMP A INNER JOIN
                    <br />
                    VCB_FEE B ON A.CCYCD = B.CCYCD
                    <br />
                    )T INNER JOIN BRANCH K ON
                    <br />
                    LPAD(T.BRANCH,5,&#39;&#39;0&#39;&#39;) = LPAD(K.SIBS_BANK_CODE,5,&#39;&#39;0&#39;&#39;)
                    <br />
                    GROUP BY K.BRAN_NAME, T.BRANCH, T.CCYCD
                    <br />
                    ORDER BY K.BRAN_NAME&#39;;<br />
                    END IF;
                    <br />
                    OPEN RefCurBK02 FOR strSql;<br />
                    <br />
                    END VCB_FEE_CAL;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;VCB_FEE_CAL_DETAIL&quot; (pFromDate in date,<br />
                    pToDate in date,<br />
                    pBranch in varchar,<br />
                    pFeeType in number,<br />
                    pCCYCD in varchar,<br />
                    RefCurBK02 IN OUT PKG_CR.RefCurType) IS<br />
                    vCCYCD varchar2(5);<br />
                    vBRANCH varchar2(5);<br />
                    strSql varchar2(4000);<br />
                    vFR_DATE NUMBER(10);<br />
                    vTO_DATE NUMBER(10);<br />
                    BEGIN<br />
                    vFR_DATE := TO_NUMBER(TO_CHAR(pFromDate, &#39;YYYYMMDD&#39;));<br />
                    vTO_DATE := TO_NUMBER(TO_CHAR(pToDate, &#39;YYYYMMDD&#39;));<br />
                    if upper(trim(pBranch)) = &#39;ALL&#39; or trim(pBranch) is null then<br />
                    vBRANCH := &#39;%&#39;;<br />
                    else<br />
                    vBRANCH := LPAD(pBranch, 5, &#39;0&#39;);<br />
                    end if;<br />
                    if upper(trim(pCCYCD)) = &#39;ALL&#39; or trim(pCCYCD) is null then<br />
                    vCCYCD := &#39;%&#39;;<br />
                    else<br />
                    vCCYCD := trim(pCCYCD);<br />
                    end if;<br />
                    --XOA DU LIEU BANG TEMP<br />
                    DELETE FROM VCB_FEE_CAL_TEMP;<br />
                    ----------------------------------<br />
                    --INSERT DU LIEU VAO BANG TEMP<br />
                    INSERT INTO VCB_FEE_CAL_TEMP<br />
                    (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)<br />
                    SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT,<br />
                    decode(msg_src,2,branch_a,<br />
                    lpad(to_number(substr(RM_NUMBER,1, length(RM_NUMBER)-12)),5,&#39;0&#39;))<br />
                    FROM VCB_MSG_CONTENT<br />
                    WHERE TRANSDATE &gt;= vFR_DATE<br />
                    AND TRANSDATE &lt;= vTO_DATE<br />
                    AND STATUS = 1<br />
                    AND BRANCH_A LIKE vBRANCH<br />
                    AND CCYCD LIKE vCCYCD<br />
                    AND msg_src &lt;&gt; 2
                    <br />
                    AND MSG_DIRECTION = &#39;SIBS-VCB&#39;;<br />
                    INSERT INTO VCB_FEE_CAL_TEMP<br />
                    (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)<br />
                    SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT,<br />
                    decode(msg_src,2,branch_a,<br />
                    lpad(to_number(substr(RM_NUMBER,1, length(RM_NUMBER)-12)),5,&#39;0&#39;))<br />
                    FROM VCB_MSG_ALL<br />
                    WHERE TRANSDATE &gt;= vFR_DATE<br />
                    AND TRANSDATE &lt;= vTO_DATE<br />
                    AND STATUS = 1<br />
                    AND BRANCH_A LIKE vBRANCH<br />
                    AND CCYCD LIKE vCCYCD<br />
                    AND msg_src &lt;&gt; 2
                    <br />
                    AND MSG_DIRECTION = &#39;SIBS-VCB&#39;;<br />
                    INSERT INTO VCB_FEE_CAL_TEMP<br />
                    (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, BRANCH)<br />
                    SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT,<br />
                    decode(msg_src,2,branch_a,<br />
                    lpad(to_number(substr(RM_NUMBER,1, length(RM_NUMBER)-12)),5,&#39;0&#39;))<br />
                    FROM VCB_MSG_ALL_HIS<br />
                    WHERE TRANSDATE &gt;= vFR_DATE<br />
                    AND TRANSDATE &lt;= vTO_DATE<br />
                    AND STATUS = 1<br />
                    AND BRANCH_A LIKE vBRANCH<br />
                    AND CCYCD LIKE vCCYCD<br />
                    AND msg_src &lt;&gt; 2
                    <br />
                    AND MSG_DIRECTION = &#39;SIBS-VCB&#39;;<br />
                    --TINH PHI VOI LOAI PHI CO DINH<br />
                    IF pFeeType = 1 THEN<br />
                    strSql := &#39;SELECT T.AMOUNT, T.FEE,T.MSG_ID, 1 AS NUM,<br />
                    K.BRAN_NAME, T.BRANCH, T.CCYCD,T.TRANS_DATE<br />
                    FROM<br />
                    (SELECT A.MSG_ID,A.AMOUNT, NVL(B.FIXEDFEE,0) FEE,<br />
                    A.BRANCH,A.CCYCD,A.TRANS_DATE<br />
                    FROM VCB_FEE_CAL_TEMP A INNER JOIN<br />
                    VCB_FEE B ON A.CCYCD=B.CCYCD<br />
                    )T INNER JOIN BRANCH K ON<br />
                    LPAD(T.BRANCH,5,&#39;&#39;0&#39;&#39;) = LPAD(K.SIBS_BANK_CODE,5,&#39;&#39;0&#39;&#39;)
                    <br />
                    ORDER BY T.TRANS_DATE,K.BRAN_NAME&#39;;<br />
                    --TINH PHI THEO TY LE<br />
                    ELSE<br />
                    strSql := &#39;SELECT T.AMOUNT, T.FEE,T.MSG_ID, 1 AS NUM,<br />
                    K.BRAN_NAME, T.BRANCH, T.CCYCD,T.TRANS_DATE<br />
                    FROM<br />
                    (SELECT A.MSG_ID,A.AMOUNT,<br />
                    (CASE WHEN NVL(B.RATE,0) * A.AMOUNT/100&gt;= NVL(B.MAXFEE,0) THEN<br />
                    NVL(B.MAXFEE,0) WHEN NVL(B.RATE,0) * A.AMOUNT/100&lt;= NVL(B.MINFEE,0) THEN<br />
                    NVL(B.MINFEE,0) ELSE NVL(B.RATE,0) * A.AMOUNT/100 END)as FEE,<br />
                    A.BRANCH,A.CCYCD,A.TRANS_DATE<br />
                    FROM VCB_FEE_CAL_TEMP A INNER JOIN<br />
                    VCB_FEE B ON A.CCYCD = B.CCYCD<br />
                    )T INNER JOIN BRANCH K ON<br />
                    LPAD(T.BRANCH,5,&#39;&#39;0&#39;&#39;) = LPAD(K.SIBS_BANK_CODE,5,&#39;&#39;0&#39;&#39;)
                    <br />
                    ORDER BY T.TRANS_DATE,K.BRAN_NAME&#39;;<br />
                    END IF;<br />
                    OPEN RefCurBK02 FOR strSql;<br />
                    <br />
                    END VCB_FEE_CAL_DETAIL;<br />
                    /<br />
                    <br />
                    <br />
                    CREATE OR REPLACE PROCEDURE &quot;VCB_FEE_CAL_DETAIL_EXCEL&quot; (pFromDate in date,<br />
                    pToDate in date,<br />
                    pBranch in varchar,<br />
                    pFeeType in number,<br />
                    pCCYCD in varchar,<br />
                    RefCurBK02 IN OUT PKG_CR.RefCurType) IS<br />
                    vCCYCD varchar2(5);<br />
                    vBRANCH varchar2(5);<br />
                    strSql varchar2(4000);<br />
                    vFR_DATE NUMBER(10);<br />
                    vTO_DATE NUMBER(10);<br />
                    BEGIN<br />
                    vFR_DATE := TO_NUMBER(TO_CHAR(pFromDate, &#39;YYYYMMDD&#39;));<br />
                    vTO_DATE := TO_NUMBER(TO_CHAR(pToDate, &#39;YYYYMMDD&#39;));<br />
                    if upper(trim(pBranch)) = &#39;ALL&#39; or trim(pBranch) is null then<br />
                    vBRANCH := &#39;%&#39;;<br />
                    else<br />
                    vBRANCH := LPAD(pBranch, 5, &#39;0&#39;);<br />
                    end if;<br />
                    if upper(trim(pCCYCD)) = &#39;ALL&#39; or trim(pCCYCD) is null then<br />
                    vCCYCD := &#39;%&#39;;<br />
                    else<br />
                    vCCYCD := trim(pCCYCD);<br />
                    end if;<br />
                    --XOA DU LIEU BANG TEMP<br />
                    DELETE FROM VCB_FEE_CAL_TEMP;<br />
                    ----------------------------------<br />
                    --INSERT DU LIEU VAO BANG TEMP<br />
                    INSERT INTO VCB_FEE_CAL_TEMP<br />
                    (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, FIELD20, BRANCH)<br />
                    SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, FIELD20,
                    <br />
                    decode(msg_src,2,branch_a,<br />
                    lpad(to_number(substr(RM_NUMBER,1, length(RM_NUMBER)-12)),5,&#39;0&#39;))<br />
                    FROM VCB_MSG_CONTENT<br />
                    WHERE TRANSDATE &gt;= vFR_DATE<br />
                    AND TRANSDATE &lt;= vTO_DATE<br />
                    AND STATUS = 1<br />
                    AND BRANCH_A LIKE vBRANCH<br />
                    AND CCYCD LIKE vCCYCD<br />
                    AND msg_src = 2<br />
                    AND MSG_DIRECTION = &#39;SIBS-VCB&#39;;<br />
                    INSERT INTO VCB_FEE_CAL_TEMP<br />
                    (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, FIELD20, BRANCH)<br />
                    SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, FIELD20,
                    <br />
                    decode(msg_src,2,branch_a,<br />
                    lpad(to_number(substr(RM_NUMBER,1, length(RM_NUMBER)-12)),5,&#39;0&#39;))<br />
                    FROM VCB_MSG_ALL<br />
                    WHERE TRANSDATE &gt;= vFR_DATE<br />
                    AND TRANSDATE &lt;= vTO_DATE<br />
                    AND STATUS = 1<br />
                    AND BRANCH_A LIKE vBRANCH<br />
                    AND CCYCD LIKE vCCYCD<br />
                    AND msg_src = 2<br />
                    AND MSG_DIRECTION = &#39;SIBS-VCB&#39;;<br />
                    INSERT INTO VCB_FEE_CAL_TEMP<br />
                    (MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, FIELD20, BRANCH)<br />
                    SELECT MSG_ID, AMOUNT, CCYCD, TRANS_DATE, CONTENT, FIELD20,
                    <br />
                    decode(msg_src,2,branch_a,<br />
                    lpad(to_number(substr(RM_NUMBER,1, length(RM_NUMBER)-12)),5,&#39;0&#39;))<br />
                    FROM VCB_MSG_ALL_HIS<br />
                    WHERE TRANSDATE &gt;= vFR_DATE<br />
                    AND TRANSDATE &lt;= vTO_DATE<br />
                    AND STATUS = 1<br />
                    AND BRANCH_A LIKE vBRANCH<br />
                    AND CCYCD LIKE vCCYCD<br />
                    AND msg_src = 2<br />
                    AND MSG_DIRECTION = &#39;SIBS-VCB&#39;;<br />
                    --TINH PHI VOI LOAI PHI CO DINH<br />
                    IF pFeeType = 1 THEN<br />
                    strSql := &#39;SELECT T.AMOUNT, T.FEE,T.MSG_ID, 1 AS NUM,<br />
                    K.BRAN_NAME, T.BRANCH, T.CCYCD,T.TRANS_DATE, T.FIELD20<br />
                    FROM<br />
                    (SELECT A.MSG_ID,A.AMOUNT, NVL(B.FIXEDFEE,0) FEE,<br />
                    A.BRANCH,A.CCYCD,A.TRANS_DATE, A.FIELD20<br />
                    FROM VCB_FEE_CAL_TEMP A INNER JOIN<br />
                    VCB_FEE B ON A.CCYCD=B.CCYCD<br />
                    )T INNER JOIN BRANCH K ON<br />
                    LPAD(T.BRANCH,5,&#39;&#39;0&#39;&#39;) = LPAD(K.SIBS_BANK_CODE,5,&#39;&#39;0&#39;&#39;)<br />
                    ORDER BY T.TRANS_DATE,K.BRAN_NAME,T.FIELD20&#39;;<br />
                    --TINH PHI THEO TY LE<br />
                    ELSE<br />
                    strSql := &#39;SELECT T.AMOUNT, T.FEE,T.MSG_ID, 1 AS NUM,<br />
                    K.BRAN_NAME, T.BRANCH, T.CCYCD,T.TRANS_DATE, T.FIELD20<br />
                    FROM<br />
                    (SELECT A.MSG_ID,A.AMOUNT,<br />
                    (CASE WHEN NVL(B.RATE,0) * A.AMOUNT/100&gt;= NVL(B.MAXFEE,0) THEN<br />
                    NVL(B.MAXFEE,0) WHEN NVL(B.RATE,0) * A.AMOUNT/100&lt;= NVL(B.MINFEE,0) THEN<br />
                    NVL(B.MINFEE,0) ELSE NVL(B.RATE,0) * A.AMOUNT/100 END)as FEE,<br />
                    A.BRANCH,A.CCYCD,A.TRANS_DATE,A.FIELD20<br />
                    FROM VCB_FEE_CAL_TEMP A INNER JOIN<br />
                    VCB_FEE B ON A.CCYCD = B.CCYCD<br />
                    )T INNER JOIN BRANCH K ON<br />
                    LPAD(T.BRANCH,5,&#39;&#39;0&#39;&#39;) = LPAD(K.SIBS_BANK_CODE,5,&#39;&#39;0&#39;&#39;)<br />
                    ORDER BY T.TRANS_DATE,K.BRAN_NAME,T.FIELD20&#39;;<br />
                    END IF;<br />
                    OPEN RefCurBK02 FOR strSql;<br />
                    <br />
                    END VCB_FEE_CAL_DETAIL_EXCEL;<br />
                    /<br />
                    <br />
                </td>
                <td>
                    <asp:DropDownList ID="ddlTad" runat="server" Width="140px" 
                         TabIndex="8" Style="max-height:50px; min-height:50px; 
                        orphans:10; widows:10">
                    </asp:DropDownList>   
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Ngân hàng nhận"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtReceiver" runat="server" MaxLength="12" Width="140px" 
                        Height="22px" TabIndex="9"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Phân hệ"></asp:Label>                           
                </td>
                <td>
                    <asp:DropDownList ID="ddlDepartment" runat="server" Height="16px" Width="140px" 
                         TabIndex="10">
                    </asp:DropDownList>   
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label16" runat="server" Text="Số bút toán"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtGwTransNum" runat="server" Height="22px" MaxLength="30" 
                        TabIndex="11" Width="140px"></asp:TextBox>
                    <asp:CheckBox ID="chkGwTransNum" runat="server" Text="Chính xác" />
                </td>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Chiều điện"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlMsgDirection" runat="server" Width="140px" 
                        Height="16px" TabIndex="12" >
                    </asp:DropDownList> 
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label17" runat="server" Text="Số hiệu giao dịch"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTransNum" runat="server" Height="22px" MaxLength="30" 
                        TabIndex="15" Width="140px"></asp:TextBox>
                    <asp:CheckBox ID="chkTransNum" runat="server" Text="Chính xác" />
                </td>
                <td>
                    <asp:Label ID="Label12" runat="server" Text="Trạng thái"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="140px" 
                        Height="16px" TabIndex="16">
                    </asp:DropDownList> 
                </td>
            </tr>
            <tr align="left">
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Số RM"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRM" runat="server" MaxLength="20" Width="140px" 
                        Height="22px" TabIndex="17"></asp:TextBox>
                    <asp:CheckBox ID="chkRM" runat="server" Text="Chính xác" />
                </td>
                <td>
                    
                    <asp:Label ID="Label14" runat="server" Text="Trạng thái forward"></asp:Label>
                    
                </td>
                <td>
                    
                    <asp:DropDownList ID="ddlStatusForward" runat="server" Width="140px" 
                        Height="16px" TabIndex="18">
                    </asp:DropDownList> 
                    
                </td>
            </tr>            
            <tr align="left">
                <td>
                    <asp:Label ID="Label11" runat="server" Text="Nguồn điện"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlMsg_src" runat="server" Width="140px" 
                        Height="16px" TabIndex="18">
                    </asp:DropDownList> 
                </td>
                <td>                    
                    <asp:Label ID="Label20" runat="server" Text="Trạng thái in điện"></asp:Label>                    
                </td>
                <td>
                    
                    <asp:DropDownList ID="ddlPrintSTS" runat="server" Width="140px" 
                        Height="16px" TabIndex="18">
                    </asp:DropDownList> 
                    
                </td>
            </tr>            
            <tr align="left">
                <td colspan="2">
                    
                    <asp:Label ID="Label15" runat="server" Text="Tổng số điện: " Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblTotal" runat="server" Font-Bold="True"></asp:Label>
                    
                </td>
                <td colspan="2">
                    
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" Width="100px" 
                        onclick="btnSearch_Click" Height="25px" TabIndex="20" />
                    <asp:Button ID="btnPrint" runat="server" Text="In điện" Width="100px" 
                        Height="25px" TabIndex="21" onclick="btnPrint_Click" Visible="True" />
                </td>                
            </tr>            
            <tr>
                <td align="center" colspan="4">
                    <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="False" 
                        Width="100%" AllowPaging="True" 
                        OnPageIndexChanging="grvData_PageIndexChanging"   
                        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="3" GridLines="Vertical" TabIndex="22" PageSize="30" 
                        onselectedindexchanged="grvData_SelectedIndexChanged" >
                        <Columns>
                            <asp:TemplateField HeaderText="Chọn" Visible="true" >
                                <HeaderTemplate>
                                    <asp:Label ID="Chon" Text="Chọn" runat="server">
                                    </asp:Label>
                                    <asp:CheckBox ID="chkALL" 
                                    runat="server" AutoPostBack="true"
                                    OnCheckedChanged="chkALL_CheckedChanged1" /> 
                                </HeaderTemplate>                                
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1"
                                     runat="server"
                                    OnCheckedChanged="CheckBox1_CheckedChanged1" />                                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:TemplateField>                            
                            <asp:BoundField DataField="QUERY_ID" HeaderText="Query_id" Visible="False" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="70px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="RM_NUMBER" HeaderText="Số RM" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="150px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="GW_TRANS_NUM" HeaderText="Số bút toán" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="70px"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="NHGUI" HeaderText="NH gửi" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="NHNHAN" HeaderText="NH nhận" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="AMOUNT" HeaderText="Số tiền" 
                            DataFormatString="{0:###,##0.00}">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="CCYCD" HeaderText="Loại tiền">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Width="60px"  HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Chi tiết">
                                <ItemTemplate>
                                    <a href='../SearchMessage/WfrmViewMsgIBPS.aspx?mn=2101&ID=<%# Eval("MSG_ID") %>' target="_blank">
                                    Chi tiết</a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField Visible="false" >                                
                                <ItemTemplate> 
                                    <asp:Label ID="lblMsgid" runat="server" Text='<%# Bind("MSG_ID") %>'>
                                    </asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                        </Columns>                           
                        <rowstyle backcolor="#EEEEEE" forecolor="Black" />
                        <pagerstyle backcolor="#999999" forecolor="Black" horizontalalign="Center" />
                        <selectedrowstyle backcolor="#008A8C" font-bold="True" forecolor="White" />                        
                        <HeaderStyle BackColor="#3399FF" ForeColor="Yellow" />
                        <alternatingrowstyle backcolor="#DCDCDC" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="4">
                    &nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>