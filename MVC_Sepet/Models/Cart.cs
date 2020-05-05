using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Sepet.Models
{
    public class Cart
    {
        Dictionary<int, CartItem> _myCart = new Dictionary<int, CartItem>();

       

        public List<CartItem> myCart
        {
            get
            {
                return _myCart.Values.ToList();

            }

        }
        public void AddItem(CartItem item)
        {
            if (_myCart.ContainsKey(item.ID))
            {
                _myCart[item.ID].Quantity += 1;
                return;
            }
            _myCart.Add(item.ID, item);
        }
        public void ClearItems()
        {
            myCart.Clear();

        }
        public decimal? TotalPrice()
        {
            return myCart.Sum(i => i.Price * i.Quantity);

        }
        public void Delete(int id)
        {
            _myCart.Remove(id);
        }

      
    }
}