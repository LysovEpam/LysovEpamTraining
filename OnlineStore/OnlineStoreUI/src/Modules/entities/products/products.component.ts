import { OnInit, Component } from '@angular/core';
import { ViewChild} from '@angular/core';
import { MatPaginator} from '@angular/material/paginator';
import { MatTableDataSource} from '@angular/material/table';
import { ProductCategory } from 'src/model/entities/apiEntities/productCategory';
import { MatDialog} from '@angular/material/dialog';
import { DialogData, ConfirmDialog } from '../../dialog-modules/confirm-dialog/confirm-dialog';
import { Router } from '@angular/router';
import { LocalStorageService } from 'src/services/localstorage.service';
import { ProductCategoryServerService } from 'src/services/product-category-server.service';
import { ProductInformation } from 'src/model/entities/apiEntities/productInformation';
import { MessageDialog } from 'src/Modules/dialog-modules/message-dialog/message-dialog';
import { ProductInformationServerService } from 'src/services/product-information-server.service';
import { ServerErrorsService } from 'src/services/server-errors.service';
import { Product } from 'src/model/entities/apiEntities/product';
import { ProductServerService } from 'src/services/product-server.service';
import { Subscription, identity } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { ProductSearchRequest } from 'src/model/entities/apiRequests/productSearchRequest';
import { ProductStatus } from 'src/model/entities/apiEntities/additional/productStatus';
import { CategoryDialogData, CategoryDialog } from 'src/Modules/dialog-modules/category-dialog/category-dialog';
import { ProductCartDialog } from 'src/Modules/dialog-modules/product-cart/product-cart';
import { UserRoleEnum, UserRole } from 'src/model/entities/apiEntities/additional/userRole';

enum typeActionEnum{
  changeAll,
  selectFilter,
  showProductToCart,
  undefined
}

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
}) 
export class ProductsComponent implements OnInit {

  querySubscription: Subscription;
  typeActionEnum:typeActionEnum;

  enableChangeProduct:boolean;
  enableSelectProduct:boolean;
  enableRemoveProduct:boolean;


  productMessage:string;

  displayedColumns: string[] = ['imageLocalSource', 'productName' , 'productPrice' , 
  'description', 'categories' , 'status' ,'actions'];
  
  dataSource: MatTableDataSource<Product>;
  
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;

  constructor(activateRoute: ActivatedRoute, private router: Router,public dialog: MatDialog, 
    private localStorageService: LocalStorageService, 
    private productServer: ProductServerService,
    private serverErrorsService: ServerErrorsService) {

      this.querySubscription = activateRoute.queryParams.subscribe(queryParam => this.processingQueryParam(queryParam));
  
    }

    ngOnInit() {

      this.dataSource = new MatTableDataSource<Product>();
      this.dataSource.paginator = this.paginator;

    }
    
    processingQueryParam(queryParam: any){

      this.typeActionEnum = typeActionEnum.undefined;
     
      var paramEntityAction = queryParam['action'];

      let userRoleName: string = this.localStorageService.getUserRole();
      let role: UserRoleEnum = UserRoleEnum[userRoleName];
      let userRoleEnum: UserRoleEnum = (<any>UserRoleEnum)[role];

      
      
      if(paramEntityAction!=null && paramEntityAction == 'changeAll')
      {
        this.typeActionEnum = typeActionEnum.changeAll;
        this.enableChangeProduct = true;
        this.enableSelectProduct = false;
        this.enableRemoveProduct = false;

        this.loadAllProducts();
      }

      else if(paramEntityAction!=null && paramEntityAction == 'selectFilter')
      {
       
        this.typeActionEnum = typeActionEnum.changeAll;
        this.enableChangeProduct = false;

        if(userRoleEnum == UserRoleEnum.Admin || 
          userRoleEnum == UserRoleEnum.Editor ||
          userRoleEnum == UserRoleEnum.User ){
            this.enableSelectProduct = true;
          }
        else{
          this.enableSelectProduct = false;
        }

        
        this.enableRemoveProduct = false;
        
        let minCost:number = Number(queryParam['minCost']);
        let maxCost:number = Number(queryParam['maxCost']);
        let productSearch:string = queryParam['productSearch'];
        let productStatuses:string[] = queryParam['productStatus'];


        let idProductCategories: number[] = queryParam['idProductCategory'];

        let idArray: number[] = [];

        idProductCategories.forEach(element => {
          idArray.push(Number(element))
        });

        let request: ProductSearchRequest = new ProductSearchRequest(minCost, maxCost, productSearch,
          productStatuses, idArray);

        this.loadSearchProducts(request);

      }
      else if(paramEntityAction!=null && paramEntityAction == 'showProductToCart'){
        
        this.typeActionEnum = typeActionEnum.changeAll;
        this.enableChangeProduct = false;
        this.enableSelectProduct = false;
        this.enableRemoveProduct = true;

        let idProducts: number[] = this.localStorageService.getProductCart();

        this.loadStorageProducts(idProducts);
      }
      else{
        this.loadAllProducts();

        this.typeActionEnum = typeActionEnum.undefined;
        this.enableChangeProduct = false;
        this.enableSelectProduct = false;
      }
  
      
    }
  
    showCategory(productCategory: ProductCategory){
  
      let dialogData: CategoryDialogData = new CategoryDialogData(productCategory.categoryName, productCategory.description, productCategory.idEntity);
      const dialogRef = this.dialog.open(CategoryDialog, {
        data: dialogData });
  
    }

    showProductDescription(product: Product){

      const dialogRef = this.dialog.open(ProductCartDialog, {
        data: product });
    }

    getStatusPrint(productStatus:ProductStatus):string{

      return ProductStatus.getStatusPrint(productStatus.status);

    }
  
    createProduct(){
      
      this.router.navigate(['/product'], {
        queryParams:{
          'action': 'create'}
      });
  
    }
  
    updateProduct(product:Product){
      
      this.router.navigate(['/product'], {
        queryParams:{
          'action': 'update',
          'idEntity': product.idEntity}
      });
    }

    removeProductCart(product:Product){
      this.localStorageService.deleteProductCart(product.idEntity);

      let idProducts: number[] = this.localStorageService.getProductCart();

      this.loadStorageProducts(idProducts);

    }
  
    deleteProduct(product:Product){

      this.productMessage = 'delete product';


       let dialogData: DialogData = new DialogData('Delete product?', 'Confirm the deletion of the product');
     
       const dialogRef = this.dialog.open(ConfirmDialog, { data: dialogData });
  
      dialogRef.afterClosed().subscribe(result => {
        if(result== true)
        {
          let jwt = this.localStorageService.getJwt();
          this.productServer.delete(product.idEntity, jwt).subscribe((data:any) =>{
            
            this.productMessage = 'Product successfully deleted';
            this.loadAllProducts();
          },
          error => { 
            this.productMessage = this.serverErrorsService.processError(error);
          } );
        }
      });
  
    }

    addToCart(product:Product){

      let productCart:number[] = this.localStorageService.getProductCart();

      if(productCart.indexOf(product.idEntity) > -1){
        this.productMessage = 'The product is already in the cart';
        return;
      }

      this.localStorageService.addToCart(product.idEntity);

      this.productMessage = 'Product successfully added to cart';

    }
  
    loadAllProducts(){
  
      this.productServer.getList().subscribe((data:Product[]) =>{
        this.dataSource = new MatTableDataSource<Product>(data);
        this.dataSource.paginator = this.paginator;
      },
      error => { 
        this.productMessage = this.serverErrorsService.processError(error);
      });
  
    }

    loadSearchProducts(productSearchRequest:ProductSearchRequest){
      this.productServer.search(productSearchRequest).subscribe((data:Product[]) =>{
        this.dataSource = new MatTableDataSource<Product>(data);
        this.dataSource.paginator = this.paginator;
      },
      error => { 
        this.productMessage = this.serverErrorsService.processError(error);
      });
    }

    loadStorageProducts(idProducts: number[]){

      if(idProducts == null || idProducts.length == 0) {
        this.productMessage = 'Shopping basket is empty';
        this.dataSource = new MatTableDataSource<Product>([]);
        this.dataSource.paginator = this.paginator;

        return;
      }

      this.productServer.getByIdList(idProducts).subscribe((data:Product[]) =>{
        this.dataSource = new MatTableDataSource<Product>(data);
        this.dataSource.paginator = this.paginator;
      },
      error => { 
        this.productMessage = this.serverErrorsService.processError(error);
      });

    }

}
