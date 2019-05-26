using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace Assets.ECSScripts.Util
//{
//    class NetworkEventID
//    {
//    }
//}
// Network send event id

static class NCommonEID
{
    public const int LoginSID = 1000;
    public const int LoginMsgCID = 1001;
    public const int RegisterMsgCID = 1002;
}

static class NSendEID
{
    public const int LoginSID = 1000;
    public const int LoginMsgSendCID = 1001;
}

// Network receive event id
static class NReceiveEID
{
}