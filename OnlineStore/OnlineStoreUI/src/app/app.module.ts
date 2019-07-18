import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Component } from '@angular/core';
import { FormsModule }   from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import {HttpClientModule} from '@angular/common/http'
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { StoreComponent } from '../Modules/main/store/store.component';
import { NewsComponent } from '../Modules/main/news/news.component';
import { SalesComponent } from '../Modules/main/sales/sales.component';
import { ContactComponent } from '../Modules/main/contact/contact.component';
import { SingupComponent } from '../Modules/main/singup/singup.component';
import { SinginComponent } from '../Modules/main/singin/singin.component';
import { NotFoundComponent } from '../Modules/main/not-found/not-found.component';
import { UserDashboardComponent } from 'src/Modules/user-dashboards/user-dashboard/user-dashboard.component';
import { EditorDashboardComponent } from '../Modules/user-dashboards/editor-dashboard/editor-dashboard.component';
import { AdminDashboardComponent } from '../Modules/user-dashboards/admin-dashboard/admin-dashboard.component';
import { ProductCategoryComponent } from '../Modules/entities/product-category/product-category.component';
import { ProductInformationComponent } from '../Modules/entities/product-information/product-information.component';
import { ProductComponent } from '../Modules/entities/product/product.component';
import { OrderComponent } from '../Modules/entities/order/order.component';
import { ProductCategoriesComponent } from '../Modules/entities/product-categories/product-categories.component';
import { ProductInformationsComponent } from '../Modules/entities/product-informations/product-informations.component';
import { ProductsComponent } from '../Modules/entities/products/products.component';
import { OrdersComponent } from '../Modules/entities/orders/orders.component';
import { TestComponent } from '../Modules/main/test/test.component';
import { DemoMaterialModule } from 'src/material-module';
import { ConfirmDialog } from 'src/Modules/dialog-modules/confirm-dialog/confirm-dialog';
import { MessageDialog } from 'src/Modules/dialog-modules/message-dialog/message-dialog';
import { CategoryDialog } from 'src/Modules/dialog-modules/category-dialog/category-dialog';
import { ShoppingBasketComponent } from '../Modules/main/shopping-basket/shopping-basket.component';


const appRoutes: Routes = [
  {path:'', component: StoreComponent},
  {path:'news', component: NewsComponent},
  {path:'sales', component: SalesComponent},
  {path:'contact', component: ContactComponent},
  {path:'singup', component: SingupComponent},
  {path:'singin', component: SinginComponent},

  {path:'user-dashboard', component: UserDashboardComponent},
  {path:'editor-dashboard', component: EditorDashboardComponent},
  {path:'admin-dashboard', component: AdminDashboardComponent},

  {path:'product-category', component: ProductCategoryComponent},
  {path:'product-categories', component: ProductCategoriesComponent},
  {path:'product-information', component: ProductInformationComponent},
  {path:'product-informations', component: ProductInformationsComponent},
  {path:'product', component: ProductComponent},
  {path:'products', component: ProductsComponent},
  {path:'order', component: OrderComponent},
  {path:'orders', component: OrdersComponent},
    

  {path:'test', component: TestComponent},

  { path: 'store', component: StoreComponent,
      children: [ {path: 'products', component: ProductsComponent }, ]
  },

  { path: 'shopping-basket', component: ShoppingBasketComponent,
      children: [ {path: 'products', component: ProductsComponent }, ]
  },

  {path:'**', component: NotFoundComponent}
]

@NgModule({
  declarations: [
    AppComponent,
    StoreComponent,
    NotFoundComponent,
    SingupComponent,
    NewsComponent,
    SalesComponent,
    ContactComponent,
    SinginComponent,
    UserDashboardComponent,
    EditorDashboardComponent,
    AdminDashboardComponent,
    ProductCategoryComponent,
    ProductInformationComponent,
    ProductComponent,
    OrderComponent,
    ProductCategoriesComponent,
    ProductInformationsComponent,
    ProductsComponent,
    OrdersComponent,
    TestComponent ,
    ConfirmDialog,
    MessageDialog,
    CategoryDialog,
    ShoppingBasketComponent

                  
  ],
  entryComponents: [ConfirmDialog, MessageDialog, CategoryDialog],
  imports: [
   
    RouterModule,
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    
    DemoMaterialModule,

    RouterModule.forRoot(appRoutes),

  ],
   bootstrap: [AppComponent]
})
export class AppModule { }
