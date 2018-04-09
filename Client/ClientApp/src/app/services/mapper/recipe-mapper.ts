import {Recipe, RecipeData} from '../../models';
import {IDataMapper} from './data-mapper.interface';
import {Inject} from '@angular/core';

export class RecipeMapper implements IDataMapper<Recipe> {
    constructor(@Inject('RecipeDataMapper') private recipeDataMapper: IDataMapper<RecipeData>) {

    }

    getClientModel(apiModel: any): Recipe {
        return new Recipe({
            id: apiModel.id,
            dateCreated: new Date(apiModel.dateCreated),
            data: this.recipeDataMapper.getClientModel(apiModel.data)
        });
    }

    getApiModel(clientModel: Recipe): Object {
        return {
            id: clientModel.id,
            dateCreated: clientModel.dateCreated,
            data: this.recipeDataMapper.getApiModel(clientModel.data)
        };
    }
}
