﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DomainException
{
    public class MessageException
    {
        public static string ErrorIsEmpty = "Error. El texto no puede ser vacio.";
        public static string ErrorIsLongerThanTheLimit = "Error. El texto no puede ser superior de 2000 caracteres.";
        public static string ErrorNotHasCategories = "Error. No tiene categorias definidas. ";
        public static string ErrorQuantity = "Error. La cantidad de estrellas debe estar entre 1 y 5.";
        public static string ErrorPrice = "Error. El precio por noche del hospedaje debe estar entre 0 y 100000."; 
    }
}
