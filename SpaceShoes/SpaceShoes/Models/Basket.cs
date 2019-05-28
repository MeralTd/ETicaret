using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpaceShoes.Models;

namespace SpaceShoes.Models
{
    public class Basket
    {
        public Basket()
        {
            products = new List<Urunler>();
        }
        public List<Urunler> products { get; set; }

        public void Add(Urunler product)
        {
            products.Add(product);
        }

    }
}