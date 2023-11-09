import React, { useState } from 'react';

import Stack from '@mui/material/Stack';
import Popover from '@mui/material/Popover';
import TableRow from '@mui/material/TableRow';
import Checkbox from '@mui/material/Checkbox';
import MenuItem from '@mui/material/MenuItem';
import TableCell from '@mui/material/TableCell';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';

import { GetMyQuestionnaireModelType } from 'src/models/questionnaire';

import Iconify from 'src/components/iconify';

// ----------------------------------------------------------------------

export default function QuestionnaireTableRow({
  selected,
  questionnaire,
  handleClick,
  handleEditClick,
  handleDeleteClick,
  handleTableRowClick
}: QuestionnaireTableRowPropTypes) {
  const [open, setOpen] = useState(null);

  const handleOpenMenu = (event: any) => {
    setOpen(event.currentTarget);
  };

  const handleCloseMenu = () => {
    setOpen(null);
  };

  return (
    <>
      <TableRow hover tabIndex={-1} role="checkbox" selected={selected}>
        <TableCell padding="checkbox">
          <Checkbox disableRipple checked={selected} onChange={handleClick} />
        </TableCell>

        <TableCell sx={{":hover": {cursor: 'pointer'}}} onClick={() => handleTableRowClick(questionnaire)} component="th" scope="row" padding="none">
          <Stack direction="row" alignItems="center" spacing={2}>
            <Typography variant="subtitle2" noWrap>
              {questionnaire.title}
            </Typography>
          </Stack>
        </TableCell>

        <TableCell align="right">
          <IconButton onClick={handleOpenMenu}>
            <Iconify icon="eva:more-vertical-fill" />
          </IconButton>
        </TableCell>
      </TableRow>

      <Popover
        open={!!open}
        anchorEl={open}
        onClose={handleCloseMenu}
        anchorOrigin={{ vertical: 'top', horizontal: 'left' }}
        transformOrigin={{ vertical: 'top', horizontal: 'right' }}
        PaperProps={{
          sx: { width: 140 },
        }}
      >
        <MenuItem onClick={() => {handleCloseMenu();handleEditClick(questionnaire);}}>
          <Iconify icon="eva:edit-fill" sx={{ mr: 2 }} />
          Edit
        </MenuItem>

        <MenuItem onClick={() => {handleCloseMenu(); handleDeleteClick(questionnaire);}} sx={{ color: 'error.main' }}>
          <Iconify icon="eva:trash-2-outline" sx={{ mr: 2 }} />
          Delete
        </MenuItem>
      </Popover>
    </>
  );
}

export type QuestionnaireTableRowPropTypes = {
  handleClick:any 
  questionnaire: GetMyQuestionnaireModelType,
  selected: any,
  handleEditClick: any,
  handleDeleteClick: (questionnaire: GetMyQuestionnaireModelType) => void,
  handleTableRowClick: (questionnaire: GetMyQuestionnaireModelType) => void
};
