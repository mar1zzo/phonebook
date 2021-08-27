import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PhonesComponent } from './phones/phones.component';
import { TypesComponent } from './types/types.component';

const routes: Routes = [
  { path: 'phones', component: PhonesComponent },
  { path: 'types', component: TypesComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
