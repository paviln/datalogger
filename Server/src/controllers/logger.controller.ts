import {NextFunction, Request, Response} from 'express';

import Log from '../models/log';
import Logger from '../models/logger';
import Plant, {IPlant, Status} from '../models/plant';

const getLogger = async (req: Request, res: Response, next: NextFunction): Promise<void> => {
  await Logger.findById(req.params.id, (err: any, doc: any) => {
    if (err) {
      res.send(err);
    } else {
      res.status(200).json(doc);
    }
  });
};

const getLoggers = async (req: Request, res: Response, next: NextFunction): Promise<void> => {
  await Logger.find((err: any, doc: any) => {
    if (err) {
      res.send(err);
    } else {
      res.status(200).json(doc);
    }
  });
};

const create = async (req: Request, res: Response, next: NextFunction): Promise<void> => {
  Logger.create(req.body, (err: any, log: any) => {
    if (err) {
      res.status(404).send(err);
    } else {
      res.status(201).json(log);
    }
  });
};

const update = async (req: Request, res: Response, next: NextFunction): Promise<void> => {
  await Logger.findByIdAndUpdate(req.body._id, req.body, {new: true}, (err, doc) => {
    if (err) {
      res.send(err);
    } else {
      res.status(201).json(doc);
    }
  });
};

const findWarnings = async (req: Request, res: Response, next: NextFunction): Promise<void> => {
  await Plant.findOne({loggerId: req.params.id, status: Status.ACTIVE}, async (err: any, plant: IPlant) => {
    if (err) {
      res.status(404).send(err);
    } else {
      const start = new Date(new Date().getTime() - (15 * 60 * 1000));
      await Log.find({
        $and: [
          {plantId: plant.id},
          {createdAt: {$gte: start}},
        ],
      }, (err: any, logs: any) => {
        if (err) {
          res.status(404).json(err);
        } else {
          res.status(200).json(logs);
        }
      });
    }
  });
};

const getActivePlant = async (req: Request, res: Response, next: NextFunction) => {
  await Plant.findOne({
    $and: [
      {loggerId: req.params.id},
      {status: Status.ACTIVE},
    ],
  }, (err: any, plant: IPlant) => {
    if (err) {
      res.status(404).json(err);
    } else {
      res.status(200).json(plant);
    }
  });
};

export {getLogger, getLoggers, create, update, findWarnings, getActivePlant};
