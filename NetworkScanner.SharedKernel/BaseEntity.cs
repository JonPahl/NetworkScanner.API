using System;
using System.ComponentModel.DataAnnotations;

namespace NetworkScanner.SharedKernel
{
    public abstract class BaseEntity //: IComparable
    {
        //[BsonId]
        //[Key]
        [Display(Name = "ID", Order = 0)]
        public int Id { get; set; }
        //public string Id { get; set; }

        [Display(Name = "Key", Order = 1)]
        public object Key { get; set; }

        //#region Compare Code

        //public int CompareTo(object obj) => Id.CompareTo(obj);

        //public override bool Equals(object obj)
        //{
        //    if (ReferenceEquals(this, obj))
        //    {
        //        return true;
        //    }

        //    if (ReferenceEquals(obj, null))
        //    {
        //        return false;
        //    }

        //    throw new NotImplementedException();
        //}

        ///// <summary>Serves as the default hash function.</summary>
        ///// <returns>A hash code for the current object.</returns>
        //public override int GetHashCode() => throw new NotImplementedException();

        //public static bool operator ==(BaseEntity left, BaseEntity right)
        //{
        //    if (left is null)
        //    {
        //        return ReferenceEquals(right, null);
        //    }

        //    return left.Equals(right);
        //}

        //public static bool operator !=(BaseEntity left, BaseEntity right) 
        //    => !(left == right);

        //public static bool operator <(BaseEntity left, BaseEntity right) 
        //    => left is null ? right is object : left.CompareTo(right) < 0;

        //public static bool operator <=(BaseEntity left, BaseEntity right)
        //    => left is null || left.CompareTo(right) <= 0;        

        //public static bool operator >(BaseEntity left, BaseEntity right)
        //    => left is object && left.CompareTo(right) > 0;        

        //public static bool operator >=(BaseEntity left, BaseEntity right)        
        //    => left is null ? right is null : left.CompareTo(right) >= 0;
        //#endregion
    }
}
