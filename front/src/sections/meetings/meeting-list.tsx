import React from "react";

import {ContentCopy, Edit as EditIcon, Login} from '@mui/icons-material';
import { Table, Button, Tooltip, TableRow, TableBody, TableHead, TableCell, TableContainer, Typography } from "@mui/material";

import { timeSuccessAlert } from "src/utils/helpers/alert-helper";
import { API_CONSTANTS } from "src/utils/globals/api-constants";

import { MeetingModel } from "src/models/meeting";

export default function MeetingList({meetings, onMeetingClick, isArchive} : {meetings?: MeetingModel[], onMeetingClick: (meeting: MeetingModel) => void, isArchive: boolean}) {

  const getLink = (id: string) => `http://${API_CONSTANTS.clientBaseUrl}/room/${id}`;
  

  const copyLink = (id: string) => {
    const link = getLink(id);
    navigator.clipboard.writeText(link);
    timeSuccessAlert(`Copied ${link}`);
  };

  return meetings != null && meetings?.length > 0 ? (
    <TableContainer>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Title</TableCell>
              <TableCell>Assigned Time</TableCell>
              <TableCell />
            </TableRow>
          </TableHead>
          <TableBody>
            {meetings?.map((m, index) => {
              const assignedDateTime = m.assignedTime ? new Date(m.assignedTime) : null;
            return (
              <TableRow key={index}>
                <TableCell>{m.title}</TableCell>
                <TableCell>{assignedDateTime && assignedDateTime?.toLocaleDateString()} {assignedDateTime && assignedDateTime?.toLocaleTimeString()}</TableCell>
                <TableCell>
                  <Tooltip title="Edit meeting">
                    <Button color="warning" variant="text" onClick={() => onMeetingClick(m)}>
                      <EditIcon />
                    </Button>
                  </Tooltip>
                  <Tooltip title="Copy link">
                    <Button color="secondary" variant="text" onClick={() => copyLink(m.id)}>
                    <ContentCopy/>
                    </Button>
                  </Tooltip>
                  <Tooltip title="Join room">
                    <Button color="primary" variant="text" onClick={() => copyLink(m.id)}>
                      <Login />
                    </Button>
                  </Tooltip>
                </TableCell>
              </TableRow>
              )})}
          </TableBody>
        </Table>
    </TableContainer>
  ) : (
    <Typography>
      There is no records...
    </Typography>
  );
}