import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ProductService, CategoryService } from './../services';
import { IProduct } from './../interface';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html'
})
export class FormComponent implements OnInit {

    public product: IProduct = {
        code: '',
        name: '',
        price: 0,
        stock: 1,
        categoryCode: '',
        categoryName: '',
        isXml: false,
        isNew: true
    };

    @Output() onCallback = new EventEmitter();
    productForm: FormGroup;
    public messages: any[];
    public messageSuccess;
    public categories = new Array();
    loading = false;
    constructor(public fb: FormBuilder, 
        private productService: ProductService,
        private categoryService: CategoryService,
        public snackBar: MatSnackBar ) { }

    ngOnInit() {
        
        this.buildForm(this.product);
        this.onLoadCategory();
    }

    private onLoadCategory() {
        this.categoryService.getAll('').subscribe(resp => {
            this.categories = resp.data;
        });
    }

    buildForm(product: IProduct): void {
        this.product = product;
        this.productForm = this.fb.group({
            'code': [product.code, [Validators.required]],
            'name': [product.name, [Validators.required, Validators.maxLength(10)]],
            'price': [product.price, [Validators.required, Validators.maxLength(15)]],
            'stock': [product.stock, [Validators.required]],
            'categoryCode': [product.categoryCode, [Validators.required]],
            'isXml': [product.isXml]
        });
        this.productForm.valueChanges
            .subscribe(data => this.onValueChanged(data));
        this.onValueChanged();
    }
    onValueChanged(data?: any) {
        if (!this.productForm) { return; }
        const form = this.productForm;
        for (const field in this.formErrors) {
            this.formErrors[field] = '';
            const control = form.get(field);

            if (control && control.dirty && !control.valid) {
                const messages = this.validationMessages[field];
                for (const key in control.errors) {
                    this.formErrors[field] += messages[key] + ' ';
                }
            }
        }
    }

    onSave() {

        if (!this.productForm.valid) {
            return;
        }

        if (this.product.isNew) {
            this.productService.save(this.productForm.value).subscribe(resp => {
                if (resp.isValid) {
                    this.onCallback.emit();
                    this.productForm.reset();
                    this.snackBar.open('Se registro correcto', 'Cerrar');
                } else {
                    this.showMessageError(resp);
                    
                }
            });
        } else {
            this.productService.update(this.productForm.value).subscribe(resp => {
                if (resp.isValid) {
                    this.onCallback.emit();
                    this.snackBar.open('Se actualizo correcto', 'Cerrar');
                } else {
                    this.showMessageError(resp);
                }
            });
        }
       
    }

    private showMessageError(resp) {
        if (resp.messages) {
            let message = '';
            resp.messages.forEach(item => {
                message = message + ' ' + item.description;
            });
            this.snackBar.open(message, 'Cerrar');
        }
    }
    formErrors = {
        'code': '',
        'name': '',
        'price': '',
        'stock': '',
        'categoryCode': '',
        'isXml': ''
    };

    validationMessages = {
        'code': {
            'required': 'El campo Codigo es requerido',
        },
        'name': {
            'required': 'El campo Nombre es requerido',
            'maxLength': 'La longitud maxima es de 10 caracteres'
        },
        'price': {
            'required': 'El campo precio es requerido'
        },
        'stock': {
            'required': 'El campo Stock Emision es requerido'
        },
        'categoryCode': {
            'required': 'El campo Categoria es requerido'
        }
    };
	
}
