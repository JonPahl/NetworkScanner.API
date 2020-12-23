﻿using NetworkScanner.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetworkScanner.Domain.Entities
{
    public class FoundDevice : BaseEntity, IComparable
    {
        [Display(Name = "DEVICE ID", Order = 4)]
        public string DeviceId { get; set; }

        [Display(Name = "DEVICE NAME", Order = 3)]
        public string DeviceName { get; set; }

        [Display(Name = "IP ADDRESS", Order = 2)]
        public string IpAddress { get; set; }

        [Display(Name = "TIMESTAMP", Order = 6)]
        public DateTime? FoundAt { get; set; }

        [Display(Name = "FOUND USING", Order = 5)]
        public string FoundUsing { get; set; }
        public FoundDevice()
        {
            DeviceId = string.Empty;
            DeviceName = string.Empty;
            FoundUsing = string.Empty;
            FoundAt = DateTime.Now;
        }
        public override int GetHashCode()
        {
            try
            {
                var txt = $"{DeviceId}_{DeviceName}";
                return txt.GetHashCode(StringComparison.OrdinalIgnoreCase);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return 0;
            }
        }
        public int GenerateId()
            => (!DeviceId.Equals("N/A"))
            ? DeviceId.GetHashCode() : IpAddress.GetHashCode();

        public override string ToString()
        {
            var str = new StringBuilder();
            foreach (var prop in GetType().GetProperties())
            {
                str.Append(prop.GetValue(this));
            }
            return str.ToString();
        }

        public int CompareTo(object obj) => Id.CompareTo(obj);
    }

    public class Details
    {
        public Details() => Cnt = 3;
        public int Cnt { get; set; }
    }
}
