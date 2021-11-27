using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPlanner.Enums
{
    public enum TYPE
    {
        MEAT,
        FISH,
        VEGETABLE,
        FRUIT,
        SWEET,
    }

    public enum AMOUNT_TYPE
    {
        EL,
        GRAMM,
        LITER,
        PCK,
    }

    public enum REFRESH_TYPE
    {        
        BUYLIST,
        INGREDIENT,
        FOOD,
        RECIPE,
    }

    public enum ALTER_TYPE
    {
        ADD,
        SUB,
        REPLACE,
    }
}
