## # SharePoint 2010 Belge Kitaplık Verilerini Harici MSSQL Veritabanına Aktarma Aracı

## Amaç
Bu uygulama, **SharePoint 2010** üzerinde bulunan “**Belge Kitaplıklarını**” harici bir **MSSQL** veri tabanına hiyerarşik olarak aktarmak amacı ile geliştirmiş bir araçtır.

## Neden?
Büyük ölçekli SharePoint projelerinde, ölçeklemesi yapılmamış veya yapılması unutulmuş liste/doküman *(Large List)* kütüphanelerinin gün geçtikçe büyümesi, altyapı değişikliğine gidilmesi ve **eski verilerin aktarımlarının istenmesi** nedeni ile geliştirilmiştir.

## Geliştirme
Uygulama, **C#** dili ile geliştirilmiş ve uzak makineden kütüphanelere erişmesi amacı ile S**harePoint 2010 CSOM***(Clien Side Object Model)* kullanılmıştır. Kullanım kolaylığı, sadeliği ve performansı açısından beğenilerek kullanılan, “Micro ORM’lerin kralı” olarak tabir edebileceğimiz **Dapper.NET** SQL işlemleri için tercih edilmiş, **UnitOfWork tasarım kalıbı** ile desteklenmiştir.

### Eksikler/Geliştirilmesi Devam Edilen:
1. Belge kitaplıklarına ait **PDF** *[byte]*’lerin aktarımları 
2. Dokümanlara ait versiyon aktarımları

## Sonuç
Bu uygulama ile **5 milyon satırlık veri** aktarımı tamamlanmış, **yaklaşık 3.5/4TB’lık doküman verisi** harici diske yazılmıştır.

## Önizleme
![SharePoint 2010 Belge Kitaplık Verilerini Harici MSSQL Veritabanına Aktarma Aracı](https://raw.githubusercontent.com/tahaipek/SharePoint2010ExportToExternalDb/master/Preview.gif)

