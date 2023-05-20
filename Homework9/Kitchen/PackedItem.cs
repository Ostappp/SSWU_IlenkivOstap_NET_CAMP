using Homework9.MenuElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework9.Kitchen
{
    internal class PackedItem
    {
        public int RecipeId { get; private set; }
        public IOffer OfferType { get; private set; }
        public uint Count { get; private set; }
        public PackedItem(int recipeId)
        {
            RecipeId = recipeId;
        }
        public void AddElement(IOffer offer, uint count = 1)
        {
            if(Count == 0)
            {
                OfferType = offer;
                Count = count;
            }
            else if(OfferType.GetType() == offer.GetType())
            {
                Count += count;
            }
        }
    }
}
