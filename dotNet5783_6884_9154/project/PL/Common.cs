using BO;
using DalFacade.DO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public static class Common
    {
        public static PO.Cart ConvertToPoCart(BO.Cart Bc, PO.Cart Pc)
        {

            Pc.CustomerAddress = Bc.CustomerAddress;
            Pc.CustomerEmail = Bc.CustomerEmail;
            Pc.CustomerName = Bc.CustomerName;
            Pc.Items = convertItemsToPOOI(Bc.items);
            Pc.TotalPrice = Bc.TotalPrice;

            return Pc;
        }
        public static List<PO.OrderItem> convertItemsToPOOI(List<BO.OrderItem> oil)
        {
            List<PO.OrderItem> returnlist = new();
            oil.ForEach(item =>
            {
                PO.OrderItem item2 = new()
                {
                    ID = item.ID,
                    Amount = item.Amount,

                    Price = item.Price,
                    ProductID = item.ProductID,
                    ProductName = item.ProductName,
                    TotalPrice = item.TotalPrice
                };
                returnlist.Add(item2);
            });
            return returnlist;
        }
        public static BO.Cart ConvertToBoCart(PO.Cart Bp)
        {
            BO.Cart item = new()
            {
                CustomerAddress = Bp.CustomerAddress,
                CustomerEmail = Bp.CustomerEmail,
                CustomerName = Bp.CustomerName,
                items = convertItemsToBOOI(Bp.Items),
                TotalPrice = Bp.TotalPrice,
            };
            return item;
        }

        public static List<BO.OrderItem> convertItemsToBOOI(List<PO.OrderItem> oil)
        {
            List<BO.OrderItem> returnlist = new();
            oil.ForEach(item =>
            {
                BO.OrderItem item2 = new()
                {
                    ID = item.ID,
                    Amount = item.Amount,

                    Price = item.Price,
                    ProductID = item.ProductID,
                    ProductName = item.ProductName,
                    TotalPrice = item.TotalPrice
                };
                returnlist.Add(item2);
            });
            return returnlist;
        }
        public static PO.OrderItem ConvertToPoItem(BO.OrderItem Pp)
        {
            PO.OrderItem item = new()
            {
                ID = Pp.ID,
                ProductID = Pp.ProductID,
                ProductName = Pp.ProductName,
                Price = Pp.Price,
                TotalPrice = Pp.TotalPrice,
                Amount = Pp.Amount
            };
            return item;
        }
        public static PO.Product ConvertToPoPro(BO.Product Pp)
        {
            PO.Product item = new()
            {
                ID = Pp.ID,
                Name = Pp.Name,
                Price = Pp.Price,
                Category = (BO.Category)(eCategory)Pp.Category,
                inStock = Pp.inStock
            };
            return item;
        }
        public static PO.ProductItem ConvertToPoProFL(BO.ProductItem Pp)
        {
            PO.ProductItem item = new()
            {
                ID = Pp.ID,
                Name = Pp.Name,
                Price = Pp.Price,
                Category = Pp.Category,
                InStock = Pp.inStock
            };
            return item;
        }


        public static ObservableCollection<PO.ProductItem> ConvertToPoProList(IEnumerable<List<BO.ProductItem>> Blist, ObservableCollection<PO.ProductItem> Plist)
        {
            Plist.Clear();
            Blist.FirstOrDefault(group =>
            {
                group.ForEach(item => Plist.Add(ConvertToPoProFL(item))); return false;
            });
            return Plist;
        }

        public static ObservableCollection<PO.ProductItem> ConvertToPoProList(IEnumerable<BO.ProductItem> Blist, ObservableCollection<PO.ProductItem> Plist)
        {
            Plist.Clear();
            Blist.FirstOrDefault(i => { Plist.Add(ConvertToPoProFL(i)); return false; });

            return Plist;
        }

        public static PO.Order ConvertToPoOrder(BO.Order Bo, PO.Order Po)
        {
            Po.ID = Bo.ID;
            Po.CustomerName = Bo.CustomerName;
            Po.CustomerAddress = Bo.CustomerAddress;
            Po.CustomerEmail = Bo.CustomerEmail;
            Po.DeiveryDate = (DateTime?)Bo?.DeiveryDate;
            Po.ShipDate = (DateTime?)Bo.ShipDate;
            Po.OrderDate = (DateTime?)Bo?.OrderDate;
            Po.TotalPrice = Bo.TotalPrice;
            Po.Status = Bo.Status;
            Po.Items = convertToPoOiList(Bo.Items);

            return Po;
        }

        public static ObservableCollection<PO.OrderForList> convertListOrder(IEnumerable<BO.OrderForList> list2, ObservableCollection<PO.OrderForList> List_o)
        {
            PO.OrderForList i = new PO.OrderForList();
            list2.FirstOrDefault(tmp =>
            {
                i = ConvertToPoOrderForList(tmp);
                List_o.Add(i); return false;
            });

            return List_o;
        }
        public static PO.OrderForList ConvertToPoOrderForList(BO.OrderForList boo)
        {
            PO.OrderForList returnOrder = new()
            {
                ID = boo.ID,
                CustomerName = boo.CustomerName,
                TotalPrice = boo.TotalPrice,
                AmountOfItems = boo.AmountOfItems,
                Status = boo.Status,
            };
            return returnOrder;
        }
        public static PO.OrderItem converToPoOi(BO.OrderItem oi)
        {
            PO.OrderItem item = new()
            {
                Amount = oi.Amount,
                ID = oi.ID,
                ProductID = oi.ProductID,
                Price = oi.Price,
                ProductName = oi.ProductName,
                TotalPrice = oi.TotalPrice
            };
            return item;
        }
        public static List<PO.OrderItem> convertToPoOiList(List<BO.OrderItem> loi)
        {
            List<PO.OrderItem> returnList = new();
            if (loi != null)
                loi.ForEach(oi => returnList.Add(converToPoOi(oi)));
            return returnList;
        }

        public static BO.OrderItem converToBoOi(PO.OrderItem oi)
        {
            BO.OrderItem item = new()
            {
                Amount = oi.Amount,
                ID = oi.ID,
                ProductID = oi.ProductID,
                Price = oi.Price,
                ProductName = oi.ProductName,
                TotalPrice = oi.TotalPrice,
            };
            return item;
        }
        public static List<BO.OrderItem> convertToBoOiList(List<PO.OrderItem> loi)
        {
            List<BO.OrderItem> returnList = new();
            loi.ForEach(oi => returnList.Add(converToBoOi(oi)));
            return returnList;
        }
        public static ObservableCollection<PO.ProductItem> convertList(ObservableCollection<PO.ProductItem> List_p, IEnumerable<BO.ProductItem> list1)
        {
            PO.ProductItem i = new PO.ProductItem();
            list1.FirstOrDefault(tmp =>
            {
                i = ConvertToPo(tmp);
                List_p.Add(i); return false;
            });
            return List_p;
        }
        public static PO.ProductItem ConvertToPo(BO.ProductItem Bp)
        {
            PO.ProductItem item = new()
            {
                ID = Bp.ID,
                Name = Bp.Name,
                Price = Bp.Price,
                Category = Bp.Category,
                InStock = Bp.inStock

            };
            return item;
        }
        public static BO.Order ConvertToBo(PO.Order Op)
        {
            BO.Order item = new()
            {
                ID = Op.ID,
                CustomerName = Op.CustomerName,
                CustomerEmail = Op.CustomerEmail,
                CustomerAddress = Op.CustomerAddress,
                OrderDate = (DateTime?)Op.OrderDate,
                ShipDate = (DateTime?)Op.ShipDate,
                DeiveryDate = (DateTime?)Op.DeiveryDate,
                Status = Op.Status,
                TotalPrice = Op.TotalPrice,
            };
            item.Items = convertItemsToBOOI(Op.Items);
            return item;
        }

        public static PO.OrderForList ConvertPFLToP(PO.Order Pp)
        {
            PO.OrderForList item = new()
            {
                ID = Pp.ID,
                CustomerName = Pp.CustomerName,
                Status = Pp.Status,
                AmountOfItems = Pp.Items.Count,
                TotalPrice = Pp.TotalPrice,
            };
            return item;
        }
        public static BO.Product ConvertToBo(PO.Product Pp)
        {
            BO.Product item = new()
            {
                ID = Pp.ID,
                Name = Pp.Name,
                Price = Pp.Price,
                Category = (BO.Category)(eCategory)Pp.Category,
                inStock = Pp.inStock
            };
            return item;
        }

        public static PO.ProductItem ConvertPFLToP(PO.Product p)
        {
            PO.ProductItem item = new();
            item.ID = p.ID;
            item.Name = p.Name;
            item.Price = p.Price;
            item.Category = p.Category;
            item.InStock = (p.inStock == 0) ? false : true;
            return item;
        }

        public static PO.Product ConvertPToPFL(PO.ProductItem p)
        {
            PO.Product item = new();
            item.ID = p.ID;
            item.Name = p.Name;
            item.Price = p.Price;
            item.Category = (Category)p.Category;

            return item;
        }
        public static BO.ProductItem ConvertPFLToB(BO.Product p)
        {
            BO.ProductItem item = new();
            item.ID = p.ID;
            item.Name = p.Name;
            item.Price = p.Price;
            item.Category = p.Category;
            item.inStock = (p.inStock == 0) ? false : true;
            return item;
        }


    }
}
