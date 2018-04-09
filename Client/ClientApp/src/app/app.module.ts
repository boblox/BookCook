import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
//import { NavMenuComponent } from './nav-menu/nav-menu.component';
import {HomeComponent} from './components/home/home.component';

import {
    RecipesService,
    SettingsService,
    IDataMapper,
    DataMapperService,
    RecipeMapper,
    RecipeRevisionMapper,
    RecipeDataMapper
} from './services';
import {Recipe} from './models';
import {ManageRecipeComponent} from './components/manage-recipe/manage-recipe.component';
import {RecipeHistoryComponent} from './components/recipe-history/recipe-history.component';

@NgModule({
    declarations: [
        AppComponent,
        //NavMenuComponent,
        HomeComponent,
        ManageRecipeComponent,
        RecipeHistoryComponent,
    ],
    imports: [
        BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            {path: '', component: HomeComponent, pathMatch: 'full'},
            {path: 'recipe/:id/history', component: RecipeHistoryComponent, pathMatch: 'full'},
            //{ path: 'counter', component: CounterComponent },
            //{ path: 'fetch-data', component: FetchDataComponent },
        ])
    ],
    providers: [
        SettingsService,
        RecipesService,
        DataMapperService,
        //TODO: change OpaqueToken to InjectionToken
        {provide: 'RecipeMapper', useClass: RecipeMapper},
        {provide: 'RecipeDataMapper', useClass: RecipeDataMapper},
        {provide: 'RecipeRevisionMapper', useClass: RecipeRevisionMapper},
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}
