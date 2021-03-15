import { JsonController, Req, Param, Body, BodyParam, Get, Post, Put, Delete, NotFoundError } from 'routing-controllers'; import mongoose from 'mongoose';

import { Log, Logs } from '../models/log';

@JsonController('/log')
export class LogController {
    @Post()
    post(@Body() log: Log) {
      return Logs.create(log);
    }
}