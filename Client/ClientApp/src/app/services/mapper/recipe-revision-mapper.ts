import {RecipeData, RecipeRevision} from '../../models';
import {IDataMapper} from './data-mapper.interface';
import {Inject} from '@angular/core';

export class RecipeRevisionMapper implements IDataMapper<RecipeRevision> {
    constructor(@Inject('RecipeDataMapper') private recipeDataMapper: IDataMapper<RecipeData>) {

    }

    getClientModel(apiModel: any): RecipeRevision {
        return new RecipeRevision({
            id: apiModel.id,
            startDate: new Date(apiModel.startDate),
            endDate: new Date(apiModel.endDate),
            version: apiModel.version,
            data: this.recipeDataMapper.getClientModel(apiModel.data)
        });
    }

    getApiModel(clientModel: RecipeRevision): Object {
        throw new Error('Method not implemented.');
    }
}
