using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace NetTest
{
   public  class SnmpTest:ITest
    {

        string _host; string _community; string _oid;
       public SnmpTest(string host, string community, string oid)
       {
           this._host = host;
           this._community = community;
           this._oid = oid;
       }
       public bool SnmpGetTest(string host, string community, string oid)
       {

           try
           {
               SimpleSnmp snmp = new SimpleSnmp(host, community);
               if (!snmp.Valid)
               {
                   return false;
               }
               Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver1,
                                                         new string[] { oid });
               if (result == null)
               {
                   return false;
               }

               LogResult(result);
               return true;
           }
           catch (Exception ex)
           {
               return false;
           }
       }
       public bool SnmpBulkGetTest(string host, string community, string[] oids)
       {

           try
           {
               SimpleSnmp snmp = new SimpleSnmp(host, community);
               if (!snmp.Valid)
               {
                   return false;
               }
               Dictionary<Oid, AsnType> result = snmp.GetBulk(oids);
               if (result == null)
               {
                   return false;
               }

               LogResult(result);
               return true;
           }
           catch (Exception)
           {
               return false;
           }
       }
       public bool SnmpSetTest(string host, string community, string oid,string octetStromg)
       {

           try
           {
               SimpleSnmp snmp = new SimpleSnmp(host, community);
               if (!snmp.Valid)
               {
                   return false;
               }
               Dictionary<Oid, AsnType> result = snmp.Set(SnmpVersion.Ver1,
                                                           new Vb[] { 
                                                    new Vb(new Oid(oid), 
                                                           new OctetString(octetStromg))
                                                    });
               if (result == null)
               {
                   return false;
               }

               LogResult(result);
               return true;
           }
           catch (Exception ex)
           {
               return false;
           }
       }
       public bool SnmpWalkTest(string host, string community, string oid)
       {

           try
           {
               SimpleSnmp snmp = new SimpleSnmp(host, community);
               if (!snmp.Valid)
               {
                   return false;
               }
               Dictionary<Oid, AsnType> result = snmp.Walk(SnmpVersion.Ver2,
                                                         oid);
               if (result == null)
               {
                   return false;
               }
               LogResult(result);
               return true;
           }
           catch (Exception ex)
           {
               return false;
           }
       }
       private string LogResult(  Dictionary<Oid, AsnType> result)
       {
           StringBuilder sb = new StringBuilder();
           foreach (KeyValuePair<Oid, AsnType> kvp in result)
           {
              string s = string.Format("{0}: {1} {2}", kvp.Key.ToString(),
                                  SnmpConstants.GetTypeName(kvp.Value.Type),
                                   kvp.Value.ToString());
              sb.AppendLine(s);
           }
           string strResult = sb.ToString();
           return strResult;
       }
       public bool SnmpTrapTest(string receiver, int receiverPort, string community, string senderSysObjectID, string senderIpAdress, VbCollection varList)
       {
           try
           {
               TrapAgent agent = new TrapAgent();
               // Send the trap to the localhost port 162
               agent.SendV1Trap(new IpAddress(receiver), receiverPort, community,
                                new Oid(senderSysObjectID), new IpAddress(senderIpAdress),
                                SnmpConstants.LinkUp, 0, 13432, varList);
               return true;
           }
           catch
           {
               return false;
           }
       } 
       public bool BulkWalkTest(string strHost, string strCommunity, string[] strOids)
       {
           // SNMP community name
           OctetString community = new OctetString(strCommunity);
           // Define agent parameters class
           AgentParameters param = new AgentParameters(community);
           // Set SNMP version to 2 (GET-BULK only works with SNMP ver 2 and 3)
           param.Version = SnmpVersion.Ver2;
           // Construct the agent address object
           // IpAddress class is easy to use here because
           //  it will try to resolve constructor parameter if it doesn't
           //  parse to an IP address
           IpAddress agent = new IpAddress(strHost);
           bool returnValue = false;
           // Construct target
           UdpTarget target = new UdpTarget((IPAddress)agent, 161, 2000, 1);
           try
           {
               foreach (string strOid in strOids)
               {
                   // Define Oid that is the root of the MIB
                   //  tree you wish to retrieve
                   Oid rootOid = new Oid(strOid); // ifDescr

                   // This Oid represents last Oid returned by
                   //  the SNMP agent
                   Oid lastOid = (Oid)rootOid.Clone();

                   // Pdu class used for all requests
                   Pdu pdu = new Pdu(PduType.GetBulk);

                   // In this example, set NonRepeaters value to 0
                   pdu.NonRepeaters = 0;
                   // MaxRepetitions tells the agent how many Oid/Value pairs to return
                   // in the response.
                   pdu.MaxRepetitions = 5;

                   // Loop through results
                   while (lastOid != null)
                   {
                       // When Pdu class is first constructed, RequestId is set to 0
                       // and during encoding id will be set to the random value
                       // for subsequent requests, id will be set to a value that
                       // needs to be incremented to have unique request ids for each
                       // packet
                       if (pdu.RequestId != 0)
                       {
                           pdu.RequestId += 1;
                       }
                       // Clear Oids from the Pdu class.
                       pdu.VbList.Clear();
                       // Initialize request PDU with the last retrieved Oid
                       pdu.VbList.Add(lastOid);
                       // Make SNMP request
                       SnmpV2Packet result = (SnmpV2Packet)target.Request(pdu, param);
                       // You should catch exceptions in the Request if using in real application.
                       // If result is null then agent didn't reply or we couldn't parse the reply.
                       if (result != null)
                       {
                           // ErrorStatus other then 0 is an error returned by 
                           // the Agent - see SnmpConstants for error definitions
                           if (result.Pdu.ErrorStatus != 0)
                           {
                               // agent reported an error with the request
                               string errorMsg = string.Format("Error in SNMP reply. Error {0} index {1}",
                                                                  result.Pdu.ErrorStatus,
                                                                  result.Pdu.ErrorIndex);
                               throw new Exception(errorMsg);
                               lastOid = null;
                               break;
                           }
                           else
                           {
                               // Walk through returned variable bindings
                               foreach (Vb v in result.Pdu.VbList)
                               {
                                   // Check that retrieved Oid is "child" of the root OID
                                   if (rootOid.IsRootOf(v.Oid))
                                   {
                                       //Console.WriteLine("{0} ({1}): {2}",
                                       //    v.Oid.ToString(),
                                       //    SnmpConstants.GetTypeName(v.Value.Type),
                                       //    v.Value.ToString());
                                       if (v.Value.Type == SnmpConstants.SMI_ENDOFMIBVIEW)
                                           lastOid = null;
                                       else
                                           lastOid = v.Oid;
                                   }
                                   else
                                   {
                                       // we have reached the end of the requested
                                       // MIB tree. Set lastOid to null and exit loop
                                       lastOid = null;
                                   }
                               }
                           }
                       }
                       else
                       {
                           //Console.WriteLine("No response received from SNMP agent.");
                           throw new Exception("No response received from SNMP agent.");
                       }
                   }
               }
               returnValue = true;
           }
           catch(Exception ex)
           {
               returnValue = false;
           }
           finally
           {
               target.Close();
           }
           return returnValue;
       
       
       }

       public bool Test()
       {
           return SnmpWalkTest(_host, _community, _oid);
       }
    }

   //public class SnmpGetParameter
   //{
   //    string _host; string _community; string _oid;
   //    public SnmpGetParameter(string host, string community, string oid)
   //    { 
            
   //    }
   //}
}
