import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ProductComponent } from './product.component';
import { FormComponent } from './form/form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatCardModule, MatInputModule, MatIconModule, MatCheckboxModule, MatButtonModule, MatSelectModule, MatSnackBarModule } from '@angular/material';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MAT_MOMENT_DATE_FORMATS, MomentDateAdapter } from '@angular/material-moment-adapter';
import { ProductService, CategoryService } from './services';

@NgModule({
    declarations: [
        ProductComponent,
        FormComponent
    ],
    imports: [
        CommonModule,
        MatCardModule,
        MatInputModule,
        MatIconModule,
        MatCheckboxModule,
        FormsModule,
        ReactiveFormsModule,
        MatButtonModule,
        MatFormFieldModule,
        MatDatepickerModule,
        MatSelectModule,
        HttpClientModule,
        MatSnackBarModule,
        RouterModule.forChild([
            {
                path: '',
                component: ProductComponent
            },
            {
                path: 'form',
                component: FormComponent
            }

        ]),
    ],
    entryComponents: [FormComponent],
    exports: [],
    providers: [ProductService, CategoryService]

})
export class ProductModule { }
