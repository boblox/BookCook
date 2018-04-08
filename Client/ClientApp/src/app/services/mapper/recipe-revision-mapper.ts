import { RecipeRevision } from "../../models";
import { IDataMapper } from "./data-mapper.interface";

export class RecipeRevisionMapper implements IDataMapper<RecipeRevision> {
    getClientModel(apiModel: Object): RecipeRevision {
        throw new Error("Method not implemented.");
    }

    getApiModel(clientModel: RecipeRevision): Object {
        throw new Error("Method not implemented.");
    }
}
