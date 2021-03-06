﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace TimsWpfControls
{
    /// <summary>
    ///  Defines a function that is used to check if a given string represents a given object of any type.
    /// </summary>
    public interface ICompareObjectToString
    {
        /// <summary>
        /// Checks if the given input string matches to the given object
        /// </summary>
        /// <param name="input">The string to compare</param>
        /// <param name="objectToCompare">The object to compare</param>
        /// <param name="stringComparison">The <see cref="StringComparison"/> used to check if the string matches</param>
        /// <param name="stringFormat">The string format to applay</param>
        /// <returns>true if the string represents the object, otherwise fase.</returns>
        public bool CheckIfStringMatchesObject(string input, object objectToCompare, StringComparison stringComparison, string stringFormat);
    }


    [MarkupExtensionReturnType(typeof(DefaultObjectToStringComparer))]
    public class DefaultObjectToStringComparer : MarkupExtension, ICompareObjectToString
    {
        private static DefaultObjectToStringComparer _Instance;

        /// <inheritdoc/>
        public bool CheckIfStringMatchesObject(string input, object objectToCompare, StringComparison stringComparison, string stringFormat)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (objectToCompare is null)
            {
                return false;
            }

            string objectText;
            if (string.IsNullOrEmpty(stringFormat))
            {
                objectText = objectToCompare.ToString();
            }
            else if (stringFormat.Contains('{') && stringFormat.Contains('}'))
            {
                objectText = string.Format(stringFormat, objectToCompare);
            }
            else
            {
                objectText = string.Format($"{{0:{stringFormat}}}", objectToCompare);
            }

            return input.Equals(objectText, stringComparison);
        }

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _Instance ??= new DefaultObjectToStringComparer();
        }
    }
}
