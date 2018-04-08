export interface IDataMapper<T> {
    getClientModel(apiModel: any): T;

    getApiModel(clientModel: T): Object;
}
