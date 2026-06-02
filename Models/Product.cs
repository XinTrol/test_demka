using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.IO;

namespace demka_podg1.Models;

public partial class Product
{
    public string Id { get; set; } = null!;

    public int? ProductCategoryId { get; set; }

    public int? UnitsId { get; set; }

    public decimal? Price { get; set; }

    public int? SupplierId { get; set; }

    public int? ManufacturerId { get; set; }

    public int? GenderType { get; set; }

    public int? Discount { get; set; }

    public int? UnitInStock { get; set; }

    public string? Description { get; set; }

    public string? Photo { get; set; }

    public virtual GenderPosition? GenderTypeNavigation { get; set; }

    public virtual Manufacurer? Manufacturer { get; set; }

    public virtual ProductCategory? ProductCategory { get; set; }

    public virtual Supplier? Supplier { get; set; }

    public virtual Unit? Units { get; set; }

    public string CardName => $"{ProductCategory.CategoeyName} | {GenderTypeNavigation.GenderName}";

    public Bitmap PhotoPath
    {
        get
        {
            var fileName = string.IsNullOrWhiteSpace(Photo) ? "picture.png" : Photo;
            Uri uri =  new Uri($"avares://demka_podg1/Assets/{fileName}");

            return new Bitmap(AssetLoader.Open(uri));
        }
    }
    public IBrush RowBackground
    {
        get
        {
            if (UnitInStock == 0)
                return new SolidColorBrush(Colors.LightBlue);
            if (Discount > 15)
                return new SolidColorBrush(Color.Parse("#2E8B57"));
            return Brushes.Transparent;
        }
    }

    // итоговая цена со скидкой
    public decimal FinalPrice => Discount > 0
        ? (Price ?? 0) * (1 - (decimal)Discount / 100)
        : (Price ?? 0);

    // перечёркивание основной цены если есть скидка (не работает)
    public string PriceDecorations => Discount > 0 ? "Strikethrough" : "None";

    // показывать итоговую цену только если есть скидка
    public bool HasDiscount => Discount > 0;
}
