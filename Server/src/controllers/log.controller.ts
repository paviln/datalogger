import { JsonController, Req, Param, Body, BodyParam, Get, Post, Put, Delete, NotFoundError } from 'routing-controllers'; import mongoose from 'mongoose';

import Log, { ILog } from '../models/log';

@JsonController('/log')
export class LogController {
    @Post()
    post(@Body() log: ILog) {
        return Log.create(log);
    }
}