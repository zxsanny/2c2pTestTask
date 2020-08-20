import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { TransactionsComponent } from './transactions/transactions.component';
import { FileUploadComponent } from './file-upload/file-upload.component';
import { NotifierModule } from 'angular-notifier';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    TransactionsComponent,
    FileUploadComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: TransactionsComponent, pathMatch: 'full' }
    ]),
    NotifierModule.withConfig({
      theme: 'material',
      position: {
        horizontal: {
          position: "right",
          distance: 10
        }
      },
      behaviour: {
        stacking: 3
      }
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
