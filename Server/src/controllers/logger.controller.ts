import { NextFunction, Request, Response } from 'express';

import Log, { ILog } from '../models/log';
import Logger, { ILogger } from '../models/logger';

const create = async (req: Request, res: Response, next: NextFunction): Promise<void> => {
    
    let newLogger: ILogger = new Logger(req.body);
    let exsist: Boolean = await Logger.exists({ _id: newLogger._id });

    if (!exsist) {
        newLogger.save((err, log) => {
            if (err) {
                res.send(err);
            }
            res.status(201).json(log);
        });
    } else {
        res.status(404).send();
    }
};

const update = async (req: Request, res: Response, next: NextFunction): Promise<void> => {
    var id = '312312312';
    await Logger.findByIdAndUpdate(req.body._id, req.body, { new: true }, (err, doc) => {
        if (err) {
            res.send(err);
        } else {
            res.status(201).json(doc);
        }
    });
};

const findWarningsInLogs = async (req: Request, res: Response, next: NextFunction): Promise<void> => {
    let exsist: Boolean = await Logger.exists({ _id: req.body.loggerId });
    if (exsist) {
        var logs: ILog[] = await getLogsInPeriod(15);
        res.json(logs).send();
    } else {
        res.status(404).send();
    }
};

const getLogsInPeriod = async(period: number): Promise<ILog[]> => {
    var logs: ILog[];
    var start = new Date(new Date().getTime() - (period * 60 * 1000));
    await Log.find( { 'createdAt': { "$gte": start } }, (err, result) => {
        if (err) {
            logs = [];
        } else {
            logs = result;
        }
    });

    return logs;
}

export { create, update, findWarningsInLogs };