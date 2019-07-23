export class OrderSearchRequest{
    status:string;
    searchString:string;

    constructor(status:string, searchString:string){
        this.status = status;
        this.searchString = searchString;
    }
}