import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiSettingsService } from './api-settings.service';
import { ProductDataRequest } from 'src/model/entities/apiRequests/productDataRequest';
import { ProductSearchRequest } from 'src/model/entities/apiRequests/productSearchRequest';


@Injectable({
  providedIn: 'root'
})
export class ProductServerService {

    constructor(private http: HttpClient, private apiSettings:ApiSettingsService){ }
      
    getList(){
      return this.http.get(this.apiSettings.getUrlProductGetList()); 
    } 

    getById(id:number){
      return this.http.get(this.apiSettings.getUrlProductGetById()+'/' + id); 
    }

    search(searchRequest :ProductSearchRequest){
      
      return this.http.post(this.apiSettings.getUrlProductSearch(), searchRequest);
    }
    getByIdList(idProductList :number[]){
      
      return this.http.post(this.apiSettings.getUrlProductGetByIdList(), idProductList);
    }

    create(product: ProductDataRequest, jwt:string){

        var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

        const httpOptions = {
          headers: headers_object
        };

        return this.http.post(this.apiSettings.getUrlProductCreate(), product, httpOptions);

    }

    update(product: ProductDataRequest, jwt:string){

      var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

      const httpOptions = {
        headers: headers_object
      };

      return this.http.post(this.apiSettings.getUrlProductUpdate(), product, httpOptions); 
    }
    
    delete(id: number, jwt:string){

      var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

      const httpOptions = {
        headers: headers_object
      };

      return this.http.delete(this.apiSettings.getUrlProductDelete()+'/' + id, httpOptions); 
    }

}
