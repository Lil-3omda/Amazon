// import { Component } from '@angular/core';

// @Component({
//   selector: 'app-footer',
//   imports: [],
//   templateUrl: './footer.html',
//   styleUrl: './footer.css'
// })
// export class Footer {

// }


import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-footer',
  imports: [CommonModule],
  templateUrl: './footer.html',
  styleUrls: ['./footer.css']
})
export class Footer {

  footerSections = [
    {
      title: 'دعنا نساعدك',
      links: [
        'المساعدة',
        'سياسات وأسعار الشحن',
        'طلبات الإرجاع والاستبدال',
        'عمليات الاستدعاء وتنبيهات سلامة المنتج',
        'تحميل تطبيق أمازون'
      ]
    },
    {
      title: 'كن شريكاً معنا',
      links: [
        'حماية وبناء علامتك التجارية',
        'أعلن عن منتجاتك',
        'البيع على أمازون',
        'الشحن مع أمازون',
        'التوريد إلى أمازون'
      ]
    },
    {
      title: 'تسوق معنا',
      links: [
        'حسابك',
        'مشترياتك',
        'عناوينك',
        'قوائمك'
      ]
    },
    {
      title: 'اعرف المزيد عنا',
      links: [
        'معلومات عن أمازون',
        'وظائف',
        'أمازون ساينس'
      ]
    }
  ];

  additionalSections = [
    {
      title: 'جودجيدز',
      subtitle: 'المراجعات والتوصيات الخاصة بالكتب',
      links: []
    },
    {
      title: 'أمازون ويب سيرفيسز',
      subtitle: 'خدمات الحوسبة السحابية',
      links: []
    },
    {
      title: 'البيع على أمازون',
      subtitle: 'ابدأ البيع عبر الإنترنت اليوم',
      links: []
    },
    {
      title: 'Advertising أمازون',
      subtitle: 'إيجاد العملاء وجذبهم والتفاعل معهم',
      links: []
    },
    {
      title: 'شوب بوب',
      subtitle: 'خدمة عملاء تجارية للأزياء',
      links: []
    },
    {
      title: 'أليكسا',
      subtitle: 'تطبيقات قابلة للتشغيل بالصوت',
      links: []
    },
    {
      title: 'موقع IMDb',
      subtitle: 'أفلام وبرامج تلفزيونية ومشاهير',
      links: []
    },
    {
      title: 'مسموع',
      subtitle: 'استمع إلى الكتب والعروض الصوتية الأصلية',
      links: []
    }
  ];

  legalLinks = [
    'إشعارات بخصوص الاهتمامات',
    'الأسعار الخصوصية',
    'شروط الاستخدام والبيع'
  ];


scrollToTop(): void {
  window.scrollTo({
    top: 0,
    behavior: 'smooth'
  });
}


  constructor() { }
}
