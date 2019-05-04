﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace dwarfone
{
    static class DWARF
    {
        enum Tag
        {
            TAG_padding =                   0x0000,
            TAG_array_type =                0x0001,
            TAG_class_type =                0x0002,
            TAG_entry_point =               0x0003,
            TAG_enumeration_type =          0x0004,
            TAG_formal_parameter =          0x0005,
            TAG_global_subroutine =         0x0006,
            TAG_global_variable =           0x0007,
            TAG_label =                     0x000A,
            TAG_lexical_block =             0x000B,
            TAG_local_variable =            0x000C,
            TAG_member =                    0x000D,
            TAG_pointer_type =              0x000F,
            TAG_reference_type =            0x0010,
            TAG_compile_unit =              0x0011,
            TAG_string_type =               0x0012,
            TAG_structure_type =            0x0013,
            TAG_subroutine =                0x0014,
            TAG_subroutine_type =           0x0015,
            TAG_typedef =                   0x0016,
            TAG_union_type =                0x0017,
            TAG_unspecified_parameters =    0x0018,
            TAG_variant =                   0x0019,
            TAG_common_block =              0x001A,
            TAG_common_inclusion =          0x001B,
            TAG_inheritance =               0x001C,
            TAG_inlined_subroutine =        0x001D,
            TAG_module =                    0x001E,
            TAG_ptr_to_member_type =        0x001F,
            TAG_set_type =                  0x0020,
            TAG_subrange_type =             0x0021,
            TAG_with_stmt =                 0x0022,
            TAG_lo_user =                   0x4080,
            TAG_hi_user =                   0xFFFF
        }

        enum Form
        {
            FORM_ADDR   = 1,
            FORM_REF    = 2,
            FORM_BLOCK2 = 3,
            FORM_BLOCK4 = 4,
            FORM_DATA2  = 5,
            FORM_DATA4  = 6,
            FORM_DATA8  = 7,
            FORM_STRING = 8,
        }

        enum At
        {
            AT_sibling = 0x0010,
            AT_location = 0x0020,
            AT_name = 0x0030,
            AT_fund_type = 0x0050,
            AT_mod_fund_type = 0x0060,
            AT_user_def_type = 0x0070,
            AT_mod_u_d_type = 0x0080,
            AT_ordering = 0x0090,
            AT_subscr_data = 0x00A0,
            AT_byte_size = 0x00B0,
            AT_bit_offset = 0x00C0,
            AT_bit_size = 0x00D0,
            AT_element_list = 0x00F0,
            AT_stmt_list = 0x0100,
            AT_low_pc = 0x0110,
            AT_high_pc = 0x0120,
            AT_language = 0x0130,
            AT_member = 0x0140,
            AT_discr = 0x0150,
            AT_discr_value = 0x0160,
            AT_string_length = 0x0190,
            AT_common_reference = 0x01A0,
            AT_comp_dir = 0x01B0,
            AT_const_value = 0x01C0,
            AT_containing_type = 0x01D0,
            AT_default_value = 0x01E0,
            AT_friends = 0x01F0,
            AT_inline = 0x0200,
            AT_is_optional = 0x0210,
            AT_lower_bound = 0x0220,
            AT_program = 0x0230,
            AT_private = 0x0240,
            AT_producer = 0x0250,
            AT_protected = 0x0260,
            AT_prototyped = 0x0270,
            AT_public = 0x0280,
            AT_pure_virtual = 0x0290,
            AT_return_addr = 0x02A0,
            AT_specification = 0x02B0,
            AT_start_scope = 0x02C0,
            AT_stride_size = 0x02E0,
            AT_upper_bound = 0x02F0,
            AT_virtual = 0x0300,
            AT_lo_user = 0x2000,
            AT_hi_user = 0x3FF0
        }

        enum Op
        {
            OP_REG = 0x01,
            OP_BASEREG = 0x02,
            OP_ADDR = 0x03,
            OP_CONST = 0x04,
            OP_DEREF2 = 0x05,
            OP_DEREF = 0x06,
            OP_DEREF4 = 0x06,
            OP_ADD = 0x07,
            OP_lo_user = 0xe0,
            OP_hi_user = 0xff
        }

        enum Ft
        {
            FT_char = 0x0001,
            FT_signed_char = 0x0002,
            FT_unsigned_char = 0x0003,
            FT_short = 0x0004,
            FT_signed_short = 0x0005,
            FT_unsigned_short = 0x0006,
            FT_integer = 0x0007,
            FT_signed_integer = 0x0008,
            FT_unsigned_integer = 0x0009,
            FT_long = 0x000a,
            FT_signed_long = 0x000b,
            FT_unsigned_long = 0x000c,
            FT_pointer = 0x000d,
            FT_float = 0x000e,
            FT_dbl_prec_float = 0x000f,
            FT_ext_prec_float = 0x0010,
            FT_complex = 0x0011,
            FT_dbl_prec_complex = 0x0012,
            FT_void = 0x0014,
            FT_boolean = 0x0015,
            FT_ext_prec_complex = 0x0016,
            FT_label = 0x0017,
            FT_lo_user = 0x8000,
            FT_hi_user = 0xffff
        }

        enum Mod
        {
            MOD_pointer_to = 0x01,
            MOD_reference_to = 0x02,
            MOD_const = 0x03,
            MOD_volatile = 0x04,
            MOD_lo_user = 0x80,
            MOD_hi_user = 0xff
        }

        enum Lang
        {
            LANG_C89 = 0x00000001,
            LANG_C = 0x00000002,
            LANG_ADA83 = 0x00000003,
            LANG_C_PLUS_PLUS = 0x00000004,
            LANG_COBOL74 = 0x00000005,
            LANG_COBOL85 = 0x00000006,
            LANG_FORTRAN77 = 0x00000007,
            LANG_FORTRAN90 = 0x00000008,
            LANG_PASCAL83 = 0x00000009,
            LANG_MODULA2 = 0x0000000a,
            LANG_lo_user = 0x00008000,
            LANG_hi_user = 0x0000ffff
        }

        enum Ord
        {
            ORD_row_major = 0,
            ORD_col_major = 1
        }

        enum Fmt
        {
            FMT_FT_C_C = 0x0,
            FMT_FT_C_X = 0x1,
            FMT_FT_X_C = 0x2,
            FMT_FT_X_X = 0x3,
            FMT_UT_C_C = 0x4,
            FMT_UT_C_X = 0x5,
            FMT_UT_X_C = 0x6,
            FMT_UT_X_X = 0x7,
            FMT_ET = 0x8
        }

        public static void DumpDWARF(ELF elf)
        {
            Console.WriteLine("DWARF v1 dump ---------------\n");
            MemoryStream elf_data = new MemoryStream(elf.elf);
            elf_data.Seek(elf.debug_offset, SeekOrigin.Begin);

            while (elf_data.Position < elf.debug_offset + elf.debug_size)
            {
                long cur_pos = elf_data.Position;
                uint size = ELF.ReadUInt32(elf_data, elf.GetEndian());

                if (size >= 6)
                {
                    ushort tag = ELF.ReadUInt16(elf_data, elf.GetEndian());
                    Console.WriteLine("\n" + (cur_pos - elf.debug_offset).ToString("x") + ": <" + size + "> " + Enum.GetName(typeof(Tag), tag));

                    while (elf_data.Position < cur_pos + size)
                    {
                        ushort at = ELF.ReadUInt16(elf_data, elf.GetEndian());
                        ulong value = 0;
                        string str = "";

                        switch (at & 0xF)
                        {
                            case (int)Form.FORM_ADDR:
                            case (int)Form.FORM_REF:
                            case (int)Form.FORM_DATA4:
                                value = ELF.ReadUInt32(elf_data, elf.GetEndian());
                                break;
                            case (int)Form.FORM_DATA2:
                                value = ELF.ReadUInt16(elf_data, elf.GetEndian());
                                break;
                            case (int)Form.FORM_DATA8:
                                value = ELF.ReadUInt64(elf_data, elf.GetEndian());
                                break;
                            case (int)Form.FORM_STRING:
                                str = ELF.ReadString(elf_data);
                                break;
                            case (int)Form.FORM_BLOCK2:
                                value = ELF.ReadUInt16(elf_data, elf.GetEndian());
                                for (uint i = 0; i < value; i++)
                                    elf_data.ReadByte();
                                break;
                            case (int)Form.FORM_BLOCK4:
                                value = ELF.ReadUInt32(elf_data, elf.GetEndian());
                                for (uint i = 0; i < value; i++)
                                    elf_data.ReadByte();
                                break;
                        }

                        switch (at & 0xF)
                        {
                            case (int)Form.FORM_ADDR:
                            case (int)Form.FORM_REF:
                            case (int)Form.FORM_DATA4:
                            case (int)Form.FORM_DATA2:
                            case (int)Form.FORM_DATA8:
                                switch (at & 0xFFF0)
                                {
                                    case (int)At.AT_language:
                                        Console.WriteLine("        " + Enum.GetName(typeof(At), at & 0xFFF0) + "(" + Enum.GetName(typeof(Lang), value) + ")");
                                        break;
                                    case (int)At.AT_fund_type:
                                        Console.WriteLine("        " + Enum.GetName(typeof(At), at & 0xFFF0) + "(" + Enum.GetName(typeof(Ft), value) + ")");
                                        break;
                                    default:
                                        Console.WriteLine("        " + Enum.GetName(typeof(At), at & 0xFFF0) + "(0x" + value.ToString("x") + ")");
                                        break;
                                }
                                break;
                            case (int)Form.FORM_STRING:
                                Console.WriteLine("        " + Enum.GetName(typeof(At), at & 0xFFF0) + "(\"" + str + "\")");
                                break;
                            case (int)Form.FORM_BLOCK2:
                            case (int)Form.FORM_BLOCK4:
                                Console.WriteLine("        " + Enum.GetName(typeof(At), at & 0xFFF0) + "(<" + value + "> TODO" + ")");
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine((cur_pos - elf.debug_offset).ToString("x") + ": <" + size + ">");
                }
            }
        }
    }
}
