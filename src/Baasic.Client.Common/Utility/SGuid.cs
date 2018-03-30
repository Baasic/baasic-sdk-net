using System;

namespace Baasic.Client.Common
{
    /// <summary>
    /// Represents a globally unique identifier (GUID) with a shorter string value Base62 Encoded.
    /// </summary>
    public struct SGuid : IComparable, IComparable<SGuid>, IEquatable<SGuid>
    {
        #region Fields

        /// <summary>
        /// A read-only instance of the ShortGuid class whose value is guaranteed to be all zeros.
        /// </summary>
        public static readonly SGuid Empty = new SGuid(Guid.Empty);

        private Guid _guid;
        private string _value;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Creates a ShortGuid from a base62 encoded string.
        /// </summary>
        /// <param name="value">The encoded guid as a base62 string</param>
        public SGuid(string value)
        {
            if (!String.IsNullOrWhiteSpace(value))
            {
                if (value.IndexOf('-') > -1)
                {
                    Guid result;
                    if (Guid.TryParse(value, out result))
                    {
                        _value = Encode(result);
                        _guid = result;
                        return;
                    }
                }
                _value = value;
                _guid = Decode(value);
            }
            else
            {
                _value = Empty.Value;
                _guid = Empty.Guid;
            }
        }

        /// <summary>
        /// Creates a ShortGuid from a Guid.
        /// </summary>
        /// <param name="guid">The Guid to encode.</param>
        public SGuid(Guid guid)
        {
            _value = Encode(guid);
            _guid = guid;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets/sets the underlying Guid
        /// </summary>
        public Guid Guid
        {
            get
            {
                return _guid;
            }
            set
            {
                if (value != _guid)
                {
                    _guid = value;
                    _value = Empty.Guid.Equals(_guid) ? Empty.Value : Encode(value);
                }
            }
        }

        /// <summary>
        /// Gets/sets the underlying base62 encoded string
        /// </summary>
        public string Value
        {
            get
            {
                return !String.IsNullOrWhiteSpace(_value) ? _value : Empty.Value;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    _value = Empty.Value;
                    _guid = Empty.Guid;
                }
                else if (value != _value)
                {
                    if (value.IndexOf('-') > -1)
                    {
                        Guid result;
                        if (Guid.TryParse(value, out result))
                        {
                            _value = Encode(result);
                            _guid = result;
                            return;
                        }
                    }
                    _value = value;
                    _guid = Decode(value);
                }
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Decodes the given base62 string
        /// </summary>
        /// <param name="value">The base62 encoded string of a Guid</param>
        /// <returns>A new Guid</returns>
        public static Guid Decode(string value)
        {
            byte[] buffer = value.FromBase62();
            try
            {
                return new Guid(buffer);
            }
            catch (ArgumentException ex)
            {
                SGuidArgumentException sguidEx = new SGuidArgumentException(value, ex);
                throw sguidEx;
            }
        }

        /// <summary>
        /// Creates a new instance of a Guid using the string value, then returns the base62 encoded version of the Guid.
        /// </summary>
        /// <param name="value">An actual Guid string (i.e. not a ShortGuid)</param>
        /// <returns></returns>
        public static string Encode(string value)
        {
            Guid guid = new Guid(value);
            return Encode(guid);
        }

        /// <summary>
        /// Encodes the given Guid as a base62 string that is 22 characters long.
        /// </summary>
        /// <param name="guid">The Guid to encode</param>
        /// <returns></returns>
        public static string Encode(Guid guid)
        {
            return guid.ToByteArray().ToBase62();
        }

        /// <summary>
        /// Implicitly converts the ShortGuid to it's Guid equivalent
        /// </summary>
        /// <param name="shortGuid"></param>
        /// <returns></returns>
        public static implicit operator Guid(SGuid shortGuid)
        {
            return shortGuid._guid;
        }

        /// <summary>
        /// Implicitly converts the string (Base62 encoded Guid or Guid) to a ShortGuid.
        /// </summary>
        /// <param name="shortGuid">Base62 encoded Guid or Guid.</param>
        /// <returns></returns>
        public static implicit operator SGuid(string shortGuid)
        {
            if (shortGuid.IndexOf('-') > -1)
            {
                Guid result;
                if (Guid.TryParse(shortGuid, out result))
                {
                    return new SGuid(result);
                }
            }
            return new SGuid(shortGuid);
        }

        /// <summary>
        /// Implicitly converts the Guid to a ShortGuid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static implicit operator SGuid(Guid guid)
        {
            return new SGuid(guid);
        }

        /// <summary>
        /// Implicitly converts the ShortGuid to it's string equivalent
        /// </summary>
        /// <param name="shortGuid"></param>
        /// <returns></returns>
        public static implicit operator string(SGuid shortGuid)
        {
            return !String.IsNullOrWhiteSpace(shortGuid._value) ? shortGuid._value : Empty.Value;
        }

        /// <summary>
        /// Initializes a new instance of the ShortGuid class
        /// </summary>
        /// <returns></returns>
        public static SGuid NewGuid()
        {
            return new SGuid(Guid.NewGuid());
        }

        /// <summary>
        /// Determines if both ShortGuids do not have the same underlying Guid value.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator !=(SGuid x, SGuid y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Determines if both ShortGuids have the same underlying Guid value.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool operator ==(SGuid x, SGuid y)
        {
            if ((object)x == null) return (object)y == null;
            return x._guid == y._guid;
        }

        /// <summary>
        /// Converts the string representation of GUId or SGUID to the equivalent <see cref="T:System.Guid" /> structure.
        /// </summary>
        /// <returns>true if the parse operation was successful; otherwise, false.</returns>
        /// <param name="input">The GUID or SGUID to convert.</param>
        /// <param name="result">The structure that will contain the parsed value.</param>
        public static bool TryParse(string input, out Guid result)
        {
            if (input.IndexOf('-') > -1)
            {
                return Guid.TryParse(input, out result);
            }
            try
            {
                result = Decode(input);
                return true;
            }
            catch { }
            result = Guid.Empty;
            return false;
        }

        public int CompareTo(object value)
        {
            if (value == null)
            {
                return 1;
            }
            Guid guid;
            if (value is string)
            {
                SGuid.TryParse((string)value, out guid);
            }
            else if (value is Guid)
            {
                guid = (Guid)value;
            }
            else if (value is SGuid)
            {
                guid = (SGuid)value;
            }
            else
            {
                throw new ArgumentException("Argument must be SGUID or GUID.");
            }

            return guid.CompareTo(this.Guid);
        }

        public int CompareTo(SGuid value)
        {
            if (value == null)
            {
                return 1;
            }

            return value.Guid.CompareTo(this.Guid);
        }

        /// <summary>
        /// Returns a value indicating whether this instance and a specified Object represent the same type and value.
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is SGuid)
                return _guid.Equals(((SGuid)obj)._guid);
            if (obj is Guid)
                return _guid.Equals((Guid)obj);
            if (obj is string && obj != null && !string.IsNullOrWhiteSpace(obj.ToString()))
            {
                Guid value = Guid.Empty;
                if (SGuid.TryParse(obj.ToString(), out value))
                {
                    return _guid.Equals(value);
                }
            }
            return false;
        }

        public bool Equals(SGuid value)
        {
            if (value == null)
            {
                return false;
            }
            return value.Guid.Equals(this.Guid);
        }

        /// <summary>
        /// Returns the HashCode for underlying Guid.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _guid.GetHashCode();
        }

        /// <summary>
        /// Returns the base62 encoded guid as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return !String.IsNullOrWhiteSpace(_value) ? _value : Empty.Value;
        }

        #endregion Methods
    }

    /// <summary>
    /// SGUID argument exception.
    /// </summary>
    public class SGuidArgumentException : ArgumentException
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SGuidArgumentException" /> class.
        /// </summary>
        /// <param name="invalidValue">The invalid value.</param>
        /// <param name="innerException">The inner exception.</param>
        public SGuidArgumentException(string invalidValue, Exception innerException)
            : base("Invalid unique identifier", innerException)
        {
            this.InvalidValue = invalidValue;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the invalid value.
        /// </summary>
        /// <value>The invalid value.</value>
        public string InvalidValue { get; set; }

        #endregion Properties
    }
}