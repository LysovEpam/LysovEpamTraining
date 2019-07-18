import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiSettingsService } from './api-settings.service';
import { ProductCategory } from 'src/model/entities/apiEntities/productCategory';

@Injectable({
  providedIn: 'root'
})
export class ProductCategoryServerService {

  
  constructor(private http: HttpClient, private apiSettings:ApiSettingsService){ }
      
    getList(){
      return this.http.get(this.apiSettings.getUrlProductCategoryGetList()); 
    }
    getById(id:number){
      return this.http.get(this.apiSettings.getUrlProductCategoryGetById()+'/' + id); 
    }
    search(search:string){
      return this.http.get(this.apiSettings.getUrlProductCategorySearch()+'/' +search); 
    }
    create(productCategory: ProductCategory, jwt:string){

        var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

        const httpOptions = {
          headers: headers_object
        };

        return this.http.post(this.apiSettings.getUrlProductCategoryCreate(), productCategory, httpOptions);

    }
    update(productCategory: ProductCategory, jwt:string){

      var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

      const httpOptions = {
        headers: headers_object
      };

      return this.http.post(this.apiSettings.getUrlProductCategoryUpdate(), productCategory, httpOptions); 
    }
    delete(id: number, jwt:string){

      var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

      const httpOptions = {
        headers: headers_object
      };

      return this.http.delete(this.apiSettings.getUrlProductCategoryDelete()+'/' + id, httpOptions); 
    }

}
