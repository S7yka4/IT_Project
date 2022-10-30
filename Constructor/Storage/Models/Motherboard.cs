using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Constructor.Storage.Models
{
    public class Motherboard:Device
    {
        public static Motherboard IdealMotherboard
        {
            get
            {
                var tmp = new Motherboard();
                tmp.FormFactor = "NanoATX";
                tmp.Socket = "-";
                tmp.MemoryType = "-";
                tmp.ECC = "-";
                tmp.RAMCount = 100;
                tmp.DriveCount = 100;
                tmp.FANCount = 100;
                return tmp;
            }
        }
        public string Socket
        { get; set; }
        public string Chipset
        { get; set; }
        public string MemoryType
        { get; set; }
        public string ECC
        { get; set; }
        public int RAMCount
        { get; set; }
        public int DriveCount
        { get; set; }
        public int FANCount
        { get; set; }
        public string FormFactor
        { get; set; }

        public Motherboard():base("Motherboard")
        {
            Socket = "";
            Chipset = "";
            MemoryType = "";
            ECC = "";
            RAMCount = 0;
            FANCount = 0;
            DriveCount = 0;
            FormFactor = "";
        }
        public Motherboard(string img,string name,string socket,string chipset,string memorytype,string ecc, int ramcount, int fancount, int drivecount, string formfactor,int count, double cost):base(name,img,count,cost,"Motherboard")
        {
            Socket = socket;
            Chipset = chipset;
            MemoryType = memorytype;
            ECC = ecc;
            RAMCount = ramcount;
            FormFactor = formfactor;
            FANCount = fancount;
            DriveCount = drivecount;
        }
    }
}
