import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ProductService } from './services';
import { IProduct } from './interface';
import { MatSnackBar } from '@angular/material';
import { FormComponent } from './form/form.component';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {

    public filterValue = '';
    products: IProduct[];
    isXml = false
    @ViewChild(FormComponent) formComponent: FormComponent;

    private product: IProduct = {
        code: '',
        name: '',
        price: 0,
        stock: 1,
        categoryCode: '',
        categoryName: '',
        isXml: false,
        isNew: true
    };
    productForm: IProduct = this.product;
    constructor(private router: Router, private productService: ProductService,
        public snackBar: MatSnackBar) { }

    ngOnInit() {
        this.onSearch();
    }

    onSearch() {
        this.productService.search(this.filterValue, this.isXml).subscribe(resp => {
            this.products = resp.data;
        });
    }

    onNew() {
        this.productForm = this.product;
        if (this.formComponent !== undefined && this.formComponent !== null)
            this.formComponent.buildForm(this.productForm);
    }

    onDelete(code: string, isXml: boolean) {
        this.productService.remove(code, isXml).subscribe(resp => {
            if (resp.isValid) {
                this.snackBar.open('Se elimino correcto', 'Cerrar');
                this.onSearch();
            }
            else {
                this.snackBar.open('Ocurrio un error al eliminar el producto', 'Cerrar');
            }
        });
    }

    onEdit(item: IProduct) {
        item.isNew = false;
        this.productForm = item;
        if (this.formComponent !== undefined && this.formComponent !== null)
            this.formComponent.buildForm(this.productForm);
    }
}
