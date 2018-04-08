import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IRecipesService } from "./recipes.service.interface";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map';
import { Recipe, RecipeRevision } from "../models";

import { ISettingsService } from "./settings.service.interface";
import { IDataMapperService } from "./mapper/data-mapper.service.interface";
import { SettingsService } from "./settings.service";
import { DataMapperService } from "./mapper/data-mapper.service";

@Injectable()
export class RecipesService implements IRecipesService {
    apiUrl: string;

    constructor(private http: HttpClient,
        private settingsService: SettingsService,
        private mapperService: DataMapperService) {
        this.apiUrl = settingsService.apiUrl
    }

    getRecipes(): Observable<Recipe[]> {
        return this.http.get(this.apiUrl + '/api/recipes')
            .map((result: any[]) => {
                return result.map(i => this.mapperService.recipeMapper.getClientModel(i));
            },
            error => console.error(error));
    }

    getRecipeRevisions(recipeId: number): Observable<RecipeRevision[]> {
        throw new Error("Method not implemented.");
    }

    saveRecipe(recipe: Recipe): Observable<Recipe> {
        return this.http.post(this.apiUrl + '/api/recipes', this.mapperService.recipeMapper.getApiModel(recipe))
            .map((result: any) => {
                return this.mapperService.recipeMapper.getClientModel(result);
            },
            error => console.error(error));
    }

    deleteRecipe(recipeId: number): Observable<boolean> {
        throw new Error("Method not implemented.");
    }
}
