## # SharePoint 2010 Belge Kitaplık Verilerini Harici MSSQL Veritabanına Aktarma Aracı

## Amaç
Bu uygulama, **SharePoint 2010** üzerinde bulunan “**Belge Kitaplıklarını**” harici bir **MSSQL** veri tabanına hiyerarşik olarak aktarmak, bu kayıtlara ait dokümanları harici bir diske yazmak ve ilişkilendirmek amacı ile geliştirmiş bir araçtır.

## Neden?
Büyük ölçekli SharePoint projelerinde, ölçeklemesi yapılmamış veya yapılması unutulmuş liste/doküman *(Large List)* kütüphanelerinin gün geçtikçe büyümesi, altyapı değişikliğine gidilmesi ve **eski verilerin aktarımlarının istenmesi** nedeni ile geliştirilmiştir.

## Geliştirme
Uygulama, **C#** dili ile geliştirilmiş ve uzak makineden kütüphanelere erişmesi amacı ile **SharePoint 2010 CSOM***(Clien Side Object Model)* kullanılmıştır. Kullanım kolaylığı, sadeliği ve performansı açısından beğenilerek kullanılan, “Micro ORM’lerin kralı” olarak tabir edebileceğimiz **Dapper.NET** SQL işlemleri için tercih edilmiş, **Repository ve UnitOfWork tasarım kalıbı** ile desteklenmiştir.

### Eksikler/Geliştirilmesi Devam Edilen:
1. Belge kitaplıklarına ait **dokümanların**'ların *[.pdf, .docx, .xls vb..]* aktarımları. *(Bu adım, dokümanları farklı senaryoya tabi tutmak zorunda kalındığı için eklenmedi.)* 
2. Dokümanlara ait versiyon aktarımları. *(İlerleyen zamanlarda talepler ve ihtiyaçlar doğrultusunda eklenebilir)*

## Sonuç
Bu uygulama ile **5 milyon satırlık veri** aktarımı tamamlanmış, **yaklaşık 3.5/4TB’lık doküman verisi** harici diske yazılmıştır.

## Kullanım
Kök dizinde yer alan **DB** klasörü altındaki veritabanı yedeği veya script'i ile veritabanınızı oluşturun. Sonrasında "**SpAktarim**" ve "**SpAktarim.Test**" projeleri içerisinde yer alan **App.config** dosyalarındaki bağlantı kodunuzu düzenleyin.

```
<connectionStrings>
	<add name="LocalDb" connectionString="Server=.;Database=SpAktarim;User Id=sa;Password=123456;" providerName="System.Data.SqlClient" />
</connectionStrings>
```

**SpAktarim.Settings.cs** sınıf dosyası içerisinde yer alan **SharePoint** sunucusuna ait bilgileri güncelleyin.

```
	public static string ServerIpAddress => "http://10.1.1.1";
	public static string ServerDomain => "SP";
	public static string ServerUserName => "spadmin";
	public static string ServerPassword => "Passw0rd";
	public static string GetFullUserName => "${ServerDomain}\\$(ServerUserName)";
```

**List<string>** tipindeki **RootFolders** static değişkenine ait verileri, yalnızca aktarılmasını istediğiniz **"Belge Kitaplıkları"**'nın adını yazarak güncelleyin.

```
    /// <summary>
    /// Aktarılması beklenen kök dizinler. Bu klasör dışındakiler aktarılmayacak.
    /// </summary>
    public static List<string> RootFolders = new List<string>
    {
        "Document Library01",
        "Document Library02",
        "Document Library03",
        "Document Library04",
        "Document Library05",
        "Document Library06",
    };
```

## Test
Solution içerisinde Unit Test oluşturulmuştur. "**SpAktarim.Test**" projesinde yer **SharePoint** ve **SQL** test sınıflarını kullanarak bağlantıları test edebilir veya kendi metodlarınızı burada yazarak hızlıca test edebilirsiniz.

## Önizleme
![SharePoint 2010 Belge Kitaplık Verilerini Harici MSSQL Veritabanına Aktarma Aracı](https://raw.githubusercontent.com/tahaipek/SharePoint2010ExportToExternalDb/master/Preview.gif)

