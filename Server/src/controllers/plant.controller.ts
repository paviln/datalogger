import {NextFunction, Request, Response} from 'express';

import Plant, {Status} from '../models/plant';
import Logger from '../models/logger';

const create = async (req: Request, res: Response, next: NextFunction) => {
  await Logger.exists({_id: req.body.loggerId}, (err, exists: Boolean) => {
    if (err) {
      res.status(404).send(err);
    } else if (exists) {
      Plant.exists({
        $and: [
          {loggerId: req.body.loggerId},
          {status: Status.ACTIVE},
        ],
      }, (err: any, exists: Boolean) => {
        if (err) {
          res.status(404).json(err);
        } else if (exists) {
          res.status(404).send();
        } else {
          const plant: any =
          {
            name: req.body.name,
            minimumTemperature: req.body.minimumTemperature,
            soilType: req.body.soilType,
            img: {
              data: req.file.buffer,
              contentType: 'image/png',
            },
            status: req.body.status,
            loggerId: req.body.loggerId,
          };
          Plant.create(plant, (err: any, plant: any) => {
            if (err) {
              res.status(404).send(err);
            }
            res.status(201).json(plant);
          });
        }
      });
    }
  });
};
export {create};
