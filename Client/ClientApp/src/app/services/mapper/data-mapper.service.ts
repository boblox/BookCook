import { Observable } from "rxjs/Observable";
import { RecipeRevision, Recipe, RecipeData } from "../../models";
import { IDataMapper } from "./data-mapper.interface";
import { Injectable, Inject } from "@angular/core";
import { IDataMapperService } from "./data-mapper.service.interface";

@Injectable()
export class DataMapperService implements IDataMapperService {
    constructor(
        @Inject('RecipeMapper') public recipeMapper: IDataMapper<Recipe>,
        @Inject('RecipeDataMapper') public recipeDataMapper: IDataMapper<RecipeData>,
        @Inject('RecipeRevisionMapper') public recipeRevisionMapper: IDataMapper<RecipeRevision>) {

    }
}
