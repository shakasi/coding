using System.Collections.Generic;
using System.Linq;

namespace UnitOfWork.ShoppingCart
{
    public class ShoppingCart : AggregateRoot
    {
        public int CustomerId { get; set; }
        public virtual Customer.Customer Customer { get; set; }

        public List<ShoppingCartLine> ShoppingCartLines { get; } = new List<ShoppingCartLine>();

        //���
        public void AddGoods(Goods.Goods goods, int quantity)
        {
            ShoppingCartLine line = ShoppingCartLines.FirstOrDefault(p => p.Goods.Id == goods.Id);
            if (line == null)
            {
                ShoppingCartLines.Add(new ShoppingCartLine()
                {
                    Goods = goods,
                    Qty = quantity,
                    ShoppingCartId = this.Id
                });
            }
            else
            {
                line.Qty += quantity;
            }
        }

        //�������+�Ż�������-�Ż��Լ�����һ��ֵ
        public void ChangeItmeQty(ShoppingCartLine cartLine, int qty)
        {
            if (qty == 0)
            {
                RemoveItem(cartLine);
            }

            cartLine.Qty = qty;
        }

        //�Ƴ�
        public void RemoveItem(ShoppingCartLine cartLine)
        {
            ShoppingCartLines.Remove(cartLine);
        }

        //���
        public void Clear()
        {
            ShoppingCartLines.Clear();
        }
    }
}