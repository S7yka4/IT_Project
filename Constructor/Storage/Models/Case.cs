using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace Constructor.Storage.Models
{
    public class Case:Device
    {
        [Required]
        [Column("StrFormFactor")]
        public string FormFactor
        { get; set; }
        [Required]
        [Column("IntFanCount")]
        public int Fan140Count
        { get; set; }
        public int Fan120Count
        { get; set; }
        public int Fan90Count
        { get; set; }
        [Required]
        [Column("IntDriveCount")]
        public int Drive25Count
        { get; set; }
        public int Drive35Count
        { get; set; }
     

        public static Case IdealCase
        {
            get
            {
                Case tmp = new Case();
                tmp.FormFactor = "ATX";
                tmp.Drive25Count = 10;
                tmp.Drive35Count = 10;
                tmp.Fan90Count = 400;
                tmp.Fan120Count = 400;
                tmp.Fan140Count = 400;
                return tmp;
            }
        }
        public Case():base("Case")
        {
            FormFactor = "";
            Fan140Count = 0;
            Fan120Count = 0;
            Fan90Count = 0;
            Drive25Count = 0;
            Drive35Count = 0;

        }

        public Case(string img, string name, string formfactor, int fan140count, int fan120count, int fan90count, int drive25count, int drive35count, int count, double cost):base(name,img,count,cost,"Case")
        {
            FormFactor = formfactor;
            Fan140Count = fan140count;
            Fan120Count = fan120count;
            Fan90Count = fan90count;
            Drive25Count = drive25count;
            Drive35Count = drive35count;
        }

        private static int GetValue(string Source)
        {
            if (Source == "ATX")
                return 3;
            if (Source == "mATX")
                return 2;
            if (Source == "MiniITX")
                return 1;
            if (Source == "NanoATX")
                return -1;
            return 0;
        }

        public static bool CompareFF(string F,string S)
        {

            if (GetValue(F) > GetValue(S))
                return true;
            else
                return false;
        }

    }
}
