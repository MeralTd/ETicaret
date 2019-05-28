using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaceShoes.Models
{
    public class VMBasket
    {
        public List<VMUrun> Urun { get; set; }
        public decimal Toplam { get; set; }
        public int SepetId { get; set; }
    }
}