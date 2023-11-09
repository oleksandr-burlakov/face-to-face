export type GenericResponse<T> = {
    errors: string[],
    result: T,
    succeeded: boolean
};