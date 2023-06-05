using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework10.Task2
{
    internal class ShippingCost : IBuyer
    {
        private decimal _defaultShippongCost = 100;
        public decimal DefaultShippingCost { get => _defaultShippongCost; }
        private (uint, uint, uint) _defaultSize;
        public (uint, uint, uint) MaxDefaultSize { get => _defaultSize; }
        public ShippingCost(decimal defaultShippingpongCost, (uint, uint, uint) defaultSize)
        {
            if (defaultShippingpongCost < 0)
                throw new ArgumentException("Shipping cost can not be less than zero");
            if (defaultSize.Item1 == 0 || defaultSize.Item2 == 0 || defaultSize.Item3 == 0)
                throw new ArgumentException("Size can not contain zero value.");

            _defaultShippongCost = defaultShippingpongCost;
            _defaultSize = defaultSize;
        }

        public decimal Buy(ElectronicDevice device)
        {
            if (device.Size.Item1 > _defaultSize.Item1 || device.Size.Item2 > _defaultSize.Item2 || device.Size.Item3 > _defaultSize.Item3)
                return (decimal)(device.Price * 0.02) + (DefaultShippingCost + (decimal)(device.Wight * 10)) * 0.3m;
            else
                return (decimal)(device.Price * 0.02) + (DefaultShippingCost + (decimal)(device.Wight * 10));
        }

        public decimal Buy(Product product)
        {
            if (product.GetDurability == Product.Durability.Low)
                return DefaultShippingCost + (decimal)product.Wight;
            else if (product.GetDurability == Product.Durability.Medium)
                return (decimal)(product.Price * 0.02) + DefaultShippingCost * 0.1m;
            else
                return (decimal)(product.Price * 0.02) + DefaultShippingCost * 0.3m;

        }
        public decimal Buy(IOffer offer)
        {
            if (offer is ElectronicDevice)
                return Buy(offer as ElectronicDevice);
            else
                return Buy(offer as Product);
        }
    }
}
