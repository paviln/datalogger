using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
   public class Plant
    {
        public int _Id{ get; set; }
        public string Name { get; set; }
        public string LoggerId { get; set; }
        public SoilType SoilType { get; set; }
        public byte[] Img { get; set; }
        public Status Status { get; set; }
        public Log[] Logs { get; set; }
    }
}
