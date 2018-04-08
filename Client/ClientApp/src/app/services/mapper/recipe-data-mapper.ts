import { Recipe, RecipeData } from "../../models";
import { IDataMapper } from "./data-mapper.interface";

export class RecipeDataMapper implements IDataMapper<RecipeData> {
    constructor() {

    }

    getClientModel(apiModel: any): RecipeData {
        return new RecipeData({
            name: apiModel.name,
            description: apiModel.description,
        });
    }

    getApiModel(clientModel: RecipeData): Object {
        throw new Error("Method not implemented.");
    }
}
