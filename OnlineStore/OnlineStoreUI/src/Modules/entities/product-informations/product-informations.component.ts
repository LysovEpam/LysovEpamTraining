import { OnInit, Component } from '@angular/core';
import { ViewChild} from '@angular/core';
import { MatPaginator} from '@angular/material/paginator';
import { MatTableDataSource} from '@angular/material/table';
import { ProductCategory } from 'src/model/entities/apiEntities/productCategory';
import { MatDialog} from '@angular/material/dialog';
import { DialogData, ConfirmDialog } from '../../dialog-modules/confirm-dialog/confirm-dialog';
import { Router } from '@angular/router';
import { LocalStorageService } from 'src/Services/localstorage.service';
import { ProductCategoryServerService } from 'src/Services/product-category-server.service';
import { ProductInformation } from 'src/model/entities/apiEntities/productInformation';
import { MessageDialog } from 'src/Modules/dialog-modules/message-dialog/message-dialog';
import { ProductInformationServerService } from 'src/Services/product-information-server.service';
import { ServerErrorsService } from 'src/Services/server-errors.service';
import { CategoryDialogData, CategoryDialog } from 'src/Modules/dialog-modules/category-dialog/category-dialog';


@Component({
  selector: 'app-product-informations',
  templateUrl: './product-informations.component.html',
  styleUrls: ['./product-informations.component.css']
})
export class ProductInformationsComponent implements OnInit {

  productInformationMessage:string;

  displayedColumns: string[] = ['idEntity', 'imageLocalSource', 'productName' , 'description', 'categories' ,'actions'];
  
  dataSource: MatTableDataSource<ProductInformation>;
  
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;


  constructor(private router: Router,public dialog: MatDialog, 
    private localStorageService: LocalStorageService, 
    private productInformationServer: ProductInformationServerService,
    private serverErrorsService: ServerErrorsService
    
    
    ) {}

  ngOnInit() {
    
    this.dataSource = new MatTableDataSource<ProductInformation>();
    this.dataSource.paginator = this.paginator;
    this.loadProductInformations();
  }
  
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  } 

  showCategoryInformation(productCategory: ProductCategory){

    let dialogData: CategoryDialogData = new CategoryDialogData(productCategory.categoryName, productCategory.description, productCategory.idEntity);
    const dialogRef = this.dialog.open(CategoryDialog, {
      data: dialogData });

  }

  createProductInformation(){
    
    this.router.navigate(['/product-information'], {
      queryParams:{
        'action': 'create'}
    });

  }

  updateProductInformation(product:ProductInformation){
    
    this.router.navigate(['/product-information'], {
      queryParams:{
        'action': 'update',
        'idEntity': product.idEntity}
    });
  }

  deleteProductInformation(product:ProductInformation){
     
    let dialogData: DialogData = new DialogData('Delete product information?', 'Confirm the deletion of the product information');
   
    const dialogRef = this.dialog.open(ConfirmDialog, { data: dialogData });

    dialogRef.afterClosed().subscribe(result => {
      if(result== true)
      {
        let jwt = this.localStorageService.getJwt();
        this.productInformationServer.delete(product.idEntity, jwt).subscribe((data:any) =>{
          
          this.productInformationMessage = 'Product information successfully deleted';
          this.loadProductInformations();
        },
        error => { 
          this.productInformationMessage = this.serverErrorsService.processError(error);
        } );
      }
    });

  }

  loadProductInformations(){

    this.productInformationServer.getList().subscribe((data:ProductInformation[]) =>{
      this.dataSource = new MatTableDataSource<ProductInformation>(data);
      this.dataSource.paginator = this.paginator;
    },
    error => { 
      this.productInformationMessage = this.serverErrorsService.processError(error);
    });

  }

}
